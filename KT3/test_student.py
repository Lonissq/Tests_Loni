"""Ваши проверки. Сначала рассчитайте ожидания по требованиям, затем пишите код."""

import unittest

from mediapolka import issue, new_state, return_loan

class StudentTests(unittest.TestCase):
    def test_initial_state(self):
        state = new_state()
        self.assertEqual(state["stock"]["mic"], 3)
        self.assertEqual(state["stock"]["tripod"], 1)
        self.assertEqual(state["stock"]["light"], 0)
        self.assertEqual(len(state["loans"]), 0)
        self.assertEqual(state["next_id"], 1)

    def test_other_items_unchanged_on_issue(self):
        state = new_state()
        issue(state, "mic", "2")
        
        self.assertEqual(state["stock"]["mic"], 1)
        self.assertEqual(state["stock"]["tripod"], 1)
        self.assertEqual(state["stock"]["light"], 0)

    def test_other_items_unchanged_on_return(self):
        state = new_state()
        res = issue(state, "mic", "2")
        loan_id = res["loan_id"]
        return_loan(state, loan_id)
        
        self.assertEqual(state["stock"]["mic"], 3)
        self.assertEqual(state["stock"]["tripod"], 1)
        self.assertEqual(state["stock"]["light"], 0)

    def test_invalid_inputs(self):
        invalid_data = ["", "lol", "3.14", "-3", "0", "13"]
        expected_message = "Введите целое количество от 1 до 3"
        
        for data in invalid_data:
            with self.subTest(data=data):
                state = new_state()
                result = issue(state, "mic", data)
                
                self.assertFalse(result["ok"])
                self.assertEqual(result["message"], expected_message)
                
                self.assertEqual(state["stock"]["mic"], 3)
                self.assertEqual(state["next_id"], 1)
                self.assertEqual(len(state["loans"]), 0)

    def test_valid_inputs(self):
        valid_data = ["1", "3", " 2 ", "0", "4"] #После исправления были добавлены проверки 0 и 4
        
        for data in valid_data:
            with self.subTest(data=data):
                state = new_state()
                result = issue(state, "mic", data)
                
                self.assertTrue(result["ok"])
                self.assertEqual(result["message"], "Выдано")

if __name__ == '__main__':
    unittest.main()
