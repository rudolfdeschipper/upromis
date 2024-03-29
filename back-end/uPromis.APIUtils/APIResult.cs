﻿namespace uPromis.APIUtils.APIMessaging
{
    public class APIResult<T, Tid>
    {
        public Tid ID { get; set; }
        public T DataSubject { get; set; }
        public string Message { get; set; }
        public object AdditionalInfo { get; set; }
    }
}
