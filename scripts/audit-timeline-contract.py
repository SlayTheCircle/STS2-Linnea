#!/usr/bin/env python3
"""Require native timeline text and both portrait contracts for each concrete chapter."""
import json
import re
from pathlib import Path
from content.models import ContentIndex

ROOT = Path(__file__).resolve().parent.parent
index = ContentIndex(ROOT)
errors = []
parents = {'ModEpochTemplate', 'CharacterUnlockEpochTemplate', 'CardUnlockEpochTemplate', 'RelicUnlockEpochTemplate', 'PotionUnlockEpochTemplate'}
chapters = [m for m in index.classes.values() if not m.abstract and any(a.parent in parents for a in index.lineage(m))]
stories = [m for m in index.classes.values() if not m.abstract and any(a.parent == 'ModStoryTemplate' for a in index.lineage(m))]
textures = json.loads((ROOT / 'assets/validation.json').read_text())['textures']
for language in ('zhs', 'eng'):
    values = json.loads((ROOT / f'localization/{language}/epochs.json').read_text())
    for model in stories:
        key = re.search(r'StoryKey\s*=>\s*"([^"]+)"', model.source)
        if not key:
            errors.append(f'{model.name}: StoryKey must be explicit')
            continue
        story = 'STORY_' + re.sub(r'[^a-zA-Z0-9]+', '_', key[1]).upper()
        if not values.get(story): errors.append(f'{language}: missing {story}')
    for model in chapters:
        match = re.search(r'\bId\s*=>\s*"([^"]+)"', model.source)
        if not match:
            errors.append(f'{model.name}: epoch Id must be explicit')
            continue
        fields = ['title', 'description', 'unlockInfo']
        if any(a.parent == 'CharacterUnlockEpochTemplate' for a in index.lineage(model)): fields.extend(['unlockText', 'unlock'])
        for field in fields:
            key = match[1] + '.' + field
            if not values.get(key): errors.append(f'{language}: missing {key}')
        for field, size in [('PackedPortraitPath', [272,174]), ('BigPortraitPath', [1672,941])]:
            path = re.search(field + r':\s*"([^"]+)"', model.source)
            if not path or textures.get(path[1], {}).get('size') != size:
                errors.append(f'{model.name}: {field} needs an explicit {size} texture contract')
for error in errors: print('错误:', error)
if errors: raise SystemExit(1)
print(f'世界线契约通过：{len(stories)}个故事、{len(chapters)}章、双语文本与两种肖像槽')
