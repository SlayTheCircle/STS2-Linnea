"""Missing event page text and variables must fail before players choose the option."""
import json
import shutil
import subprocess
import sys
import tempfile
import unittest
from pathlib import Path

ROOT = Path(__file__).resolve().parents[2]

class EventLocalizationContract(unittest.TestCase):
    def test_missing_pages_options_and_variables(self):
        manifest = next(ROOT.glob('STS2-*.json'))
        with tempfile.TemporaryDirectory(prefix='event-contract-') as directory:
            fixture = Path(directory)
            shutil.copy2(manifest, fixture / manifest.name)
            shutil.copytree(ROOT / 'scripts', fixture / 'scripts', ignore=shutil.ignore_patterns('__pycache__'))
            shutil.copytree(ROOT / 'src' / manifest.stem / 'Content', fixture / 'src' / manifest.stem / 'Content')
            shutil.copytree(ROOT / 'localization', fixture / 'localization')
            def audit():
                return subprocess.run([sys.executable, str(fixture / 'scripts/audit-event-contract.py')], capture_output=True, text=True)
            positive = audit()
            self.assertEqual(positive.returncode, 0, positive.stdout + positive.stderr)
            for language in ('zhs', 'eng'):
                path = fixture / 'localization' / language / 'events.json'
                original = json.loads(path.read_text())
                page = next(key for key in original if key.startswith('STS2_') and '.pages.' in key and '.INITIAL.' not in key)
                option = next(key for key in original if key.startswith('STS2_') and '.options.' in key and key.endswith('.description'))
                for key in (page, option):
                    with self.subTest(language=language, key=key):
                        values = original.copy()
                        del values[key]
                        path.write_text(json.dumps(values, ensure_ascii=False))
                        rejected = audit()
                        self.assertNotEqual(rejected.returncode, 0)
                        self.assertIn(key, rejected.stdout)
                values = original.copy()
                values[page] += ' {MissingEventValue}'
                path.write_text(json.dumps(values, ensure_ascii=False))
                rejected = audit()
                self.assertNotEqual(rejected.returncode, 0)
                self.assertIn('MissingEventValue', rejected.stdout)
                path.write_text(json.dumps(original, ensure_ascii=False))

if __name__ == '__main__':
    unittest.main()
