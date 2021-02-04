using System.Collections.Generic;

namespace uPromis.APIUtils.APIMessaging
{
    public class SaveMessage<T>
    {
        public int ID { get; set; }
        public T DataSubject { get; set; }
        public string Action { get; set; }
        public string SubAction { get; set; }
        public List<object> AdditionalData { get; set; }
    }
}
