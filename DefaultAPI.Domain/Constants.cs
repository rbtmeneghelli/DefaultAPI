using System;

namespace DefaultAPI.Domain
{
    public class Constants
    {
        public const int SaltSize = 16;
        public const int HashSize = 20;
        public const string DefaultPassword = "123mudar";

        public const string ServerApiKey = "AIzaSyD2i3-nX8RkclUxWPFwirDHKN_D0x2h4Pc"; // Get this from your Firebase Developer Console Login  
        public const string SenderId = "AAAANLjaZwE:APA91bFAfv1CviU_8WyiL971mnqBXi2m6qJax9VwWvmUOnMepnShnGeZmw_sBYAAe3YH5CW370xJm-LZrWCMNt5CMK_Hn8fhigbtc5OaJd0_rqubiHK4hEI4CFh179hfTmwMoHOk9QkW"; // Get this from your Firebase Developer Console Login  

        public const string UrlToGetFireBase = "https://fcm.googleapis.com/fcm/send";
        public const string UrlToGetCep = "http://viacep.com.br/ws/";
        public const string UrlToGetStates = "https://servicodados.ibge.gov.br/api/v1/localidades/estados";
        public const string UrlToGetCities = "https://servicodados.ibge.gov.br/api/v1/localidades/estados/{0}/municipios";
        public const string UrlToHangFire = "https://{url}:9000/hangfire/servers";
        public const string UrlToRabbitMQ = "https://{url}:15672";
        public const string UrlToKissLog = "https://kisslog.net/Dashboard/{KissLog.ApplicationId}/defaultapi";

        public const string SaveLog = @"insert into Log(Class,Method,Message_Error,Update_time,Object) values('{0}','{1}','{2}','{3}','{4}')";

        public const string ExceptionRequestAPI = "Erro ao efetuar request da Api externa: {0}";
        public const string ExceptionExcel = "Erro ao gerar o excel solicitado";

        public const string ErrorInAdd = "Ocorreu um erro ao adicionar um novo registro. Entre em contato com o Administrador";
        public const string ErrorInUpdate = "Ocorreu um erro ao adicionar um novo registro. Entre em contato com o Administrador";
        public const string ErrorInDelete = "Ocorreu um erro ao deletar o registro. Entre em contato com o Administrador";
        public const string ErrorInResearch = "Ocorreu um erro ao adicionar um novo registro. Entre em contato com o Administrador";
        public const string ErrorInLogin = "As autenticações fornecidas são invalidas. tente novamente!";
        public const string ErrorInChangePassword = "Ocorreu um erro ao efetuar a troca de senha";
        public const string ErrorInResetPassword = "Ocorreu um erro ao efetuar o reset de senha";
        public const string ErrorInBackup = "Ocorreu um erro para efetuar a execução do backup";
        public const string ErrorInProcedure = "Ocorreu um erro para efetuar a execução da procedure {0}";
        public const string ErrorInAddCity = "Ocorreu um erro para adicionar as cidades na base de dados";

        public const string SuccessInLogin = "A autenticação foi efetuada com sucesso";
        public const string SuccessInAdd = "O registro foi adicionado com sucesso";
        public const string SuccessInUpdate = "O registro foi atualizado com sucesso";
        public const string SuccessInDelete = "O registro foi excluído com sucesso";
        public const string SuccessInResearch = "O registro foi localizado com sucesso";
        public const string SuccessInChangePassword = "A troca de senha foi efetuada com sucesso";
        public const string SuccessInResetPassword = "O reset de senha foi efetuado com sucesso";
        public const string SuccessInProcedure = "A procedure {0} foi executada com sucesso";
        public const string SuccessInBackup = "O backup foi executado com sucesso";
        public const string SuccessInAddCity = "As cidades foram adicionadas com sucesso";
    }
}
