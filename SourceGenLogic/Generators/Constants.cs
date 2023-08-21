using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators
{
    public static class ImplementationType
    {
        public const string CQRS = nameof(CQRS);
        public const string CQRSWithRepository = nameof(CQRSWithRepository);
        public const string Repository = nameof(Repository);
    }
    public static class CQRSRequestType
    {
        public const string None = nameof(None);
        public const string Query = nameof(Query);
        public const string Command = nameof(Command);
    }


    public static class HTTPAction
    {
        public const string GET = nameof(GET);
        public const string PUT = nameof(PUT);
        public const string POST = nameof(POST);
        public const string DELETE = nameof(DELETE);
    }

   
}




