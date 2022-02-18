%% [![](https://mermaid.ink/img/pako:eNp1kEFLxDAQhf9KmJu4q_cggtSyBESKUUEpyDSZXQNtUtPJwrrsfzex66k4p8x735vAO4IJlkDCRF-JvKF7h7uIQ-tFHkwcfBo6ivM-YmRn3IieRSVwElXvyPPSbHRxNyHserrp4m2TumuduiWo9C-pvGOHvfsmneKeDpmdTHT545K-8sTChEglPd-o1uuLy0ZLoR7Vs7p7UO_1h355eq3fZr_RBcjX_yUsoWG3R6YML6ScnDVYwUBxQGdzSceitcCfNFALMj8tbTH13ELrTxlNo83p2joOEeQW-4lWUFrUB29Ackz0B52LPlOnH92KgRM)](https://mermaid.live/edit/#pako:eNp1kEFLxDAQhf9KmJu4q_cggtSyBESKUUEpyDSZXQNtUtPJwrrsfzex66k4p8x735vAO4IJlkDCRF-JvKF7h7uIQ-tFHkwcfBo6ivM-YmRn3IieRSVwElXvyPPSbHRxNyHserrp4m2TumuduiWo9C-pvGOHvfsmneKeDpmdTHT545K-8sTChEglPd-o1uuLy0ZLoR7Vs7p7UO_1h355eq3fZr_RBcjX_yUsoWG3R6YML6ScnDVYwUBxQGdzSceitcCfNFALMj8tbTH13ELrTxlNo83p2joOEeQW-4lWUFrUB29Ackz0B52LPlOnH92KgRM)
```mermaid
sequenceDiagram
    autonumber
    participant C as Client
    participant PS as Google<br>Pub/Sub
    participant ISS as InitializeSurveySubscriber<br>.net core
    C--)+PS: INITIALIZE_SURVEY
    PS--)+ISS: INITIALIZE_SURVEY
    deactivate PS
    deactivate ISS
```