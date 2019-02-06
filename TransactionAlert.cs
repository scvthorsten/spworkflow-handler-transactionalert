using log4net;
using Newtonsoft.Json;
using SmartPesa.Objects;
using SmartPesa.WorkflowLibrary;
using System.Collections.Specialized;
using System.Configuration;

namespace SmartPesa.Workflow
{
    public class TransactionAlert : DestinationBase
    {
        private NameValueCollection _pathSettings;
        private static readonly ILog _log = LogManager.GetLogger("LogFile");
        private System.Media.SoundPlayer _player;
        private string _approveSound;
        private string _declineSound;

        public TransactionAlert()
        {
            _log.Info(" -> Loading transaction alert settings");
            this._pathSettings = ConfigurationManager.GetSection("transactionAlert/pathSettings") as NameValueCollection;

            _approveSound = this._pathSettings["approveSound"];
            _log.Info("Sound approve: " + _approveSound);

            _declineSound = this._pathSettings["declineSound"];
            _log.Info("Sound decline: " + _declineSound);

            _player = new System.Media.SoundPlayer();
        }
        public override string Name()
        {
            return "TransactionAlert";
        }

        public override string Version()
        {
            return "1.0";
        }

        public override object ProcessMessage(object payload)
        {
            var newPayment = JsonConvert.DeserializeObject<Payment>((string)payload);
            _log.InfoFormat("     {0}{1}{2}", newPayment.TransactionRef, newPayment.Amount.ToString().PadLeft(20).PadRight(40), newPayment.ResponseCode);
            _player.SoundLocation = _approveSound;
            if (newPayment.ResponseCode != ResponseCodes.Approved)
            {
                _player.SoundLocation = _declineSound;
            }
            _player.Play();
            return null;
        }

        public override string Shutdown()
        {
            return null;
        }
    }
}
