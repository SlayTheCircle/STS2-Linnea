#!/usr/bin/env python3
"""Check literal event pages/options and their variables before native localization consumes them.

Convention: initial option keys are uppercase literals inside GenerateInitialOptions;
result pages use PageDescription("KEY"). This is a source contract, not an effect test.
"""
import json
import re
from pathlib import Path

import modmeta
from content.models import ContentIndex
from content.members import expression_end
from content.variables import variable_contract

ROOT = Path(__file__).resolve().parent.parent
index = ContentIndex(ROOT)
errors = []
models = index.family('event')
for model in models:
    entry = modmeta.loc_prefix(ROOT) + 'EVENT_' + modmeta.public_stem(model.name)
    required = {entry + '.title', entry + '.pages.INITIAL.description'}
    match = re.search(r'\bGenerateInitialOptions\(\)\s*\{', model.source)
    if match:
        body = model.source[match.end()-1:expression_end(model.source, match.end()-1)]
        options = set(re.findall(r'"([A-Z][A-Z0-9_]*)"', body))
        for option in options:
            required.update(entry + '.pages.INITIAL.options.' + option + '.' + field
                            for field in ('title', 'description'))
    for page in re.findall(r'\bPageDescription\("([A-Z][A-Z0-9_]*)"\)', model.source):
        required.add(entry + '.pages.' + page + '.description')
    declared, problems = variable_contract(index.lineage(model))
    errors.extend(model.name + ': ' + problem for problem in problems)
    for language in ('zhs', 'eng'):
        values = json.loads((ROOT / 'localization' / language / 'events.json').read_text())
        for key in sorted(required):
            if key not in values:
                errors.append(f'[{language}] missing event key: {key}')
        for key, value in values.items():
            if not key.startswith(entry + '.'):
                continue
            used = set(re.findall(r'\{(\w+)(?::[^}]*)?\}', value))
            if used - declared:
                errors.append(f'[{language}] {key}: undeclared event variables {sorted(used - declared)}')
for error in errors:
    print('错误:', error)
if errors:
    raise SystemExit(1)
print(f'事件页面／选项／变量契约通过（{len(models)}个事件 × 中英）')
