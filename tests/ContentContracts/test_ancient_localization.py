"""Dialogue continuation and speakers are native UI contracts, including intermediate lines."""
import json
import shutil
import subprocess
import sys
import tempfile
import unittest
from pathlib import Path
ROOT=Path(__file__).resolve().parents[2]
class AncientContract(unittest.TestCase):
    def test_missing_next_and_speaker(self):
        with tempfile.TemporaryDirectory(prefix='ancient-contract-') as directory:
            fixture=Path(directory)
            shutil.copytree(ROOT/'scripts',fixture/'scripts',ignore=shutil.ignore_patterns('__pycache__'))
            shutil.copytree(ROOT/'localization',fixture/'localization')
            def audit(): return subprocess.run([sys.executable,str(fixture/'scripts/audit-ancient-contract.py')],capture_output=True,text=True)
            self.assertEqual(audit().returncode,0)
            for language in ('zhs','eng'):
                path=fixture/f'localization/{language}/ancients.json';original=json.loads(path.read_text())
                for suffix in ('.next','.char','-visit'):
                    values=original.copy();key=next(k for k in values if k.endswith(suffix));del values[key]
                    path.write_text(json.dumps(values));rejected=audit()
                    self.assertNotEqual(rejected.returncode,0,rejected.stdout)
                path.write_text(json.dumps(original))
if __name__=='__main__': unittest.main()
