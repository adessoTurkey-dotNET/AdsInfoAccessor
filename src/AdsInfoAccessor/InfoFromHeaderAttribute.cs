using System;

namespace AdsInfoAccessor
{
    [AttributeUsage(AttributeTargets.Property)]
    public class InfoFromHeaderAttribute : InfoBaseAttribute
    {
        public InfoFromHeaderAttribute(
            string headerName)
        {
            HeaderName = headerName;
        }

        public string HeaderName { get; set; }
    }
}