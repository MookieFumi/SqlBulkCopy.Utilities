using System;
using SqlBulkCopy.Utilities.Model;

namespace SqlBulkCopy.Utilities.Test.Model
{
    public class AccessLog : IContextEntity
    {
        //Original position
        //public virtual int Id { get; set; }
        //public virtual string Ip { get; set; }
        //public virtual DateTime Date { get; set; }
        //public virtual string FullName { get; set; }

        public virtual string FullName { get; set; }
        public virtual string Ip { get; set; }
        public virtual int Id { get; set; }
        public virtual DateTime Date { get; set; }
        
    }
}