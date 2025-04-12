using System;
using System.Linq.Expressions;
using System.Reflection;

namespace AutoMapper
{
    internal class MemberArg
    {
        public MemberInfo MemberInfo { get; set; }

        public Type Type { get; set; }

        public object StaticValue { get; set; }

        public Delegate ValueDelegate { get; set; }

        public Expression Expression { get; set; }
    }
}
