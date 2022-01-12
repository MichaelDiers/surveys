import unittest

import main

class EvaluateSurveyResultTests(unittest.TestCase):
    def test_evaluate_survey_result_ok(self):
        data = {
            'participantId': 'f36bdc7d-5ea3-4864-bad7-f87f09f70213',
            #'participantId': '0f76dabd-5816-4e74-8fa7-4f0352e89d96',
            '2acd0457-f7b3-4ed4-b6d7-e66631c5b6fb': '4',
            '855fd0fc-fd9e-4c9b-a0cc-7b2ee7114dc2': '5'
        }
        
        self.assertTrue(main.evaluate_survey_result(data))

if __name__ == '__main__':
    unittest.main()