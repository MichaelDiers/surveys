import unittest

import main

class ValidateTests(unittest.TestCase):
    def test_validate_ok(self):
        data = {
            "participants":[
                {
                    "email":"foo@bar",
                    "name":"name1"
                },
                {
                    "email":"bar@foo",
                    "name":"name2"
                }
            ],
            "name":"Survey-Test",
            "status":"CREATED",
            "id":"ididid"
        }
        self.assertTrue(main.validate(data))

if __name__ == '__main__':
    unittest.main()