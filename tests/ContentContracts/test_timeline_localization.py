"""Native timeline must never display missing keys or silently omit a portrait slot."""
import json
import shutil
import subprocess
import sys
import tempfile
import unittest
from pathlib import Path

ROOT = Path(__file__).resolve().parents[2]

class TimelineLocalizationContract(unittest.TestCase):
    def test_missing_text_and_portrait_contracts(self):
        manifest = next(ROOT.glob('STS2-*.json'))
        with tempfile.TemporaryDirectory(prefix='timeline-contract-') as directory:
            fixture = Path(directory)
            shutil.copy2(manifest, fixture / manifest.name)
            for folder in ('scripts', 'localization', 'assets'):
                if folder == 'assets':
                    (fixture / folder).mkdir()
                    shutil.copy2(ROOT / folder / 'validation.json', fixture / folder / 'validation.json')
                else:
                    shutil.copytree(ROOT / folder, fixture / folder, ignore=shutil.ignore_patterns('__pycache__'))
            shutil.copytree(ROOT / 'src' / manifest.stem / 'Content', fixture / 'src' / manifest.stem / 'Content')
            def audit():
                return subprocess.run([sys.executable, str(fixture / 'scripts/audit-timeline-contract.py')], capture_output=True, text=True)
            positive = audit()
            self.assertEqual(positive.returncode, 0, positive.stdout + positive.stderr)
            for language in ('zhs', 'eng'):
                path = fixture / 'localization' / language / 'epochs.json'
                original = json.loads(path.read_text())
                for ending in ('.title', '.description', '.unlockInfo', '.unlockText', '.unlock'):
                    key = next(k for k in original if k.endswith(ending))
                    values = original.copy()
                    del values[key]
                    path.write_text(json.dumps(values, ensure_ascii=False))
                    rejected = audit()
                    self.assertNotEqual(rejected.returncode, 0)
                    self.assertIn(key, rejected.stdout)
                path.write_text(json.dumps(original, ensure_ascii=False))
            path = fixture / 'assets/validation.json'
            original = json.loads(path.read_text())
            for pattern in ('epoch_portraits', '_thumb.png'):
                values = json.loads(json.dumps(original))
                key = next(k for k in values['textures'] if pattern in k)
                del values['textures'][key]
                path.write_text(json.dumps(values))
                rejected = audit()
                self.assertNotEqual(rejected.returncode, 0)
                self.assertIn('PortraitPath', rejected.stdout)

if __name__ == '__main__':
    unittest.main()
