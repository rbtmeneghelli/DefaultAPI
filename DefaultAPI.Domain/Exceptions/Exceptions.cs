using DefaultAPI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Exceptions
{
    public sealed class ExceptionExcel : BaseException
    {
        public ExceptionExcel() : base(Constants.ExceptionExcel) { }
    }

    public sealed class ExceptionAdd : BaseException
    {
        public ExceptionAdd() : base(Constants.ErrorInAdd) { }
    }

    public sealed class ExceptionUpdate : BaseException
    {
        public ExceptionUpdate() : base(Constants.ErrorInUpdate) { }
    }

    public sealed class ExceptionDeleteLogic : BaseException
    {
        public ExceptionDeleteLogic() : base(Constants.ErrorInDeleteLogic) { }
    }

    public sealed class ExceptionDeletePhysical : BaseException
    {
        public ExceptionDeletePhysical() : base(Constants.ErrorInDeletePhysical) { }
    }

    public sealed class ExceptionResearch : BaseException
    {
        public ExceptionResearch() : base(Constants.ErrorInResearch) { }
    }

    public sealed class ExceptionGetAll : BaseException
    {
        public ExceptionGetAll() : base(Constants.ErrorInGetAll) { }
    }

    public sealed class ExceptionGetId : BaseException
    {
        public ExceptionGetId() : base(Constants.ErrorInGetId) { }
    }

    public sealed class ExceptionGetDdl : BaseException
    {
        public ExceptionGetDdl() : base(Constants.ErrorInGetDdl) { }
    }

    public sealed class ExceptionLogin : BaseException
    {
        public ExceptionLogin() : base(Constants.ErrorInLogin) { }
    }

    public sealed class ExceptionChangePassword : BaseException
    {
        public ExceptionChangePassword() : base(Constants.ErrorInChangePassword) { }
    }

    public sealed class ExceptionResetPassword : BaseException
    {
        public ExceptionResetPassword() : base(Constants.ErrorInResetPassword) { }
    }

    public sealed class ExceptionActiveRecord : BaseException
    {
        public ExceptionActiveRecord() : base(Constants.ErrorInActiveRecord) { }
    }

    public sealed class ExceptionBackup : BaseException
    {
        public ExceptionBackup() : base(Constants.ErrorInBackup) { }
    }

    public sealed class ExceptionProcedure : BaseException
    {
        public ExceptionProcedure(string procedureName) : base(string.Format(Constants.ErrorInBackup, procedureName)) { }
    }

    public sealed class ExceptionAddCity : BaseException
    {
        public ExceptionAddCity() : base(Constants.ErrorInAddCity) { }
    }

    public sealed class ExceptionRefreshToken : BaseException
    {
        public ExceptionRefreshToken() : base(Constants.ErrorInRefreshToken) { }
    }
}