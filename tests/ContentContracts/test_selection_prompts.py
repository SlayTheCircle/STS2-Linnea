"""Reject missing native selection prompts before they can throw during card play."""
import json
import re
import shutil
import subprocess
import sys
import tempfile
import unittest
from pathlib import Path

ROOT = Path(__file__).resolve().parents[2]


class SelectionPromptContract(unittest.TestCase):
    def test_each_language_requires_the_native_prompt(self):
        manifest = next(ROOT.glob("STS2-*.json"))
        mod_id = manifest.stem
        candidates = sorted(p for p in (ROOT / "src" / mod_id / "Content" / "Cards").rglob("*.cs")
                            if re.search(r"\bSelectionScreenPrompt\b", p.read_text()))
        self.assertTrue(candidates, "fixture needs a real card using the native prompt")
        name = candidates[0].stem
        stem = re.sub(r"([A-Z]+)([A-Z][a-z])", r"\1_\2", name)
        stem = re.sub(r"([a-z0-9])([A-Z])", r"\1_\2", stem).upper()
        key = mod_id.replace("-", "_").upper() + "_CARD_" + stem + ".selectionScreenPrompt"
        with tempfile.TemporaryDirectory(prefix="selection-prompts-") as directory:
            fixture = Path(directory)
            shutil.copy2(manifest, fixture / manifest.name)
            scripts = fixture / "scripts"
            scripts.mkdir()
            for filename in ("modmeta.py", "audit-loc-coverage.py"):
                shutil.copy2(ROOT / "scripts" / filename, scripts / filename)
            shutil.copytree(ROOT / "scripts" / "content", scripts / "content", ignore=shutil.ignore_patterns("__pycache__"))
            shutil.copytree(ROOT / "src" / mod_id / "Content", fixture / "src" / mod_id / "Content")
            shutil.copytree(ROOT / "localization", fixture / "localization")

            def audit():
                return subprocess.run([sys.executable, str(scripts / "audit-loc-coverage.py")], capture_output=True, text=True, check=False)

            positive = audit()
            self.assertEqual(positive.returncode, 0, positive.stdout + positive.stderr)
            for language in ("zhs", "eng"):
                with self.subTest(language=language):
                    path = fixture / "localization" / language / "cards.json"
                    values = json.loads(path.read_text())
                    original = values.pop(key)
                    path.write_text(json.dumps(values, ensure_ascii=False))
                    rejected = audit()
                    self.assertNotEqual(rejected.returncode, 0)
                    self.assertIn(key, rejected.stdout)
                    values[key] = original
                    path.write_text(json.dumps(values, ensure_ascii=False))


if __name__ == "__main__":
    unittest.main()
