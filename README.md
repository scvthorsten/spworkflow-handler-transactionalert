# spWorkflow Handler: TransactionAlert

[![license](https://img.shields.io/github/license/cosmos/cosmos-sdk.svg)](https://github.com/smartpesa/spworkflow-handler-transactionalert/master/LICENSE)

Template of handler dynamically loaded by spWorkflow using reflection of precompiled library. Spring and config match the assembly name and dynamically propogate json messages to this handler.

TransactionAlert is useful for fraud monitoring, real-time alerting and visualization of platform activity

Implementation:
  1. subscriber receives Payment class (with sub-classes of type card, qr and crypto payment)
  2. data provider receives DataProvider class with extra_data dictionary

Objective is to play a sound (approve/decline) based on transaction response code. Template serves as starting point for any extensions to dashboards, 3rd party notifications, or any transaction alerts screens
