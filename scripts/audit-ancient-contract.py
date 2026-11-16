#!/usr/bin/env python3
"""Require continuous dialogue lines, repeat markers, visit metadata and native next labels."""
import json
import re
from pathlib import Path
ROOT = Path(__file__).resolve().parent.parent
errors=[]
counts=[]
for language in ('zhs','eng'):
    values=json.loads((ROOT / f'localization/{language}/ancients.json').read_text())
    groups={}
    for key,value in values.items():
        match=re.fullmatch(r'(.+\.talk\.[^.]+\.\d+)-(\d+)(r?)\.(char|ancient|next)',key)
        if not match: continue
        group,row,repeating,field=match.groups()
        if not value: errors.append(f'{language}: empty {key}')
        groups.setdefault(group,{}).setdefault(int(row),{}).setdefault(repeating,{})[field]=key
    for group,rows in groups.items():
        if sorted(rows)!=list(range(len(rows))): errors.append(f'{language}: {group} has missing line')
        visit=group+'-visit'
        if not str(values.get(visit,'')).isdigit(): errors.append(f'{language}: missing numeric {visit}')
        markers={marker for row in rows.values() for marker in row}
        if len(markers)!=1: errors.append(f'{language}: {group} has mixed repeat markers')
        for row,variants in rows.items():
            for marker,fields in variants.items():
                if len(set(fields)&{'char','ancient'})!=1: errors.append(f'{language}: {group}-{row}{marker} needs one speaker')
                if row<len(rows)-1 and 'next' not in fields: errors.append(f'{language}: missing {group}-{row}{marker}.next')
                if row==len(rows)-1 and 'next' in fields: errors.append(f'{language}: {group}-{row}{marker} has a continuation without the next speaker')
    counts.append(len(groups))
for error in errors: print('错误:',error)
if errors: raise SystemExit(1)
print(f'先古对话契约通过：{counts[0]}组双语对话')
