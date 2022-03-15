LEMBRETES IMPORTANTES!!!

- Para executar a migration pela library Data é necessario instalar os seguintes pacotes:
1 - Microsoft.EntityFrameworkCore
2 - Microsoft.EntityFrameworkCore.SqlServer ou Npgsql.EntityFrameworkCore.PostgreSQL
3 - Microsoft.EntityFrameworkCore.Tools

Comandos basicos de migration:
1 - Abrir a janela do Package Manager Console
2 - Apontar para a library Data
3 - Criando banco de dados -> Add-Migration Initial ou Add-Migration Initial -Context ApplicationDbContext ou EntityFrameworkCore\Add-Migration InitialDb -Context WebsrvApiContext
4 - Aplicar o comando de atualização -> Update-Database ou Update-Database -Context ApplicationDbContext
5 - Remover Migration -> Remove-Migration ou Remove-Migration -Context ApplicationDbContext

Comandos extras de migration:
1 - Comando para adicionar a migration (Add-Migration CamposNovosTabelaMeta)
2 - Comando para atualizar o banco (Update-Database -v)

DataAnnotation Importante!
[NotMapped] -> Serve para não criar a propriedade da classe como campo no bd ao efetuar migration!

Relacionamentos:
Tabela Cliente >> Tabela Venda (Relacionamento 1 para muitos)
Tabela Autor >> Tabela AutorBiografia (Relacionamento 1 para 1)
Tabela LivroCategoria >> Tabela Livro e Tabela Categoria (Relacionamento N para N)

Links Uteis:
https://www.learnentityframeworkcore.com/conventions/many-to-many-relationship
http://www.macoratti.net/19/10/ang7_apinc1.htm
http://www.macoratti.net/19/04/aspc_autom1.htm
https://material.angular.io/components/categories
http://www.macoratti.net/19/10/ang7_apinc2.htm
https://www.learnentityframeworkcore.com/configuration/one-to-many-relationship-configuration
http://www.macoratti.net/19/07/c_utilweb4.htm
http://www.macoratti.net/19/09/efcore_mmr2.htm (Crud com relacionamento Many to Many)
https://www.c-sharpcorner.com/article/using-epplus-to-import-and-export-data-in-asp-net-core/
https://www.c-sharpcorner.com/article/import-and-export-data-using-epplus-core/
https://www.entityframeworktutorial.net/

Pendências do projeto:
Criar as funcionalidades da API
Utilização do AutoMapper

Configuração do projeto API:
Botão direito Properties >> Debug >> Desmarcar opção "Enable SSL"

Exemplo de connectionString do PostgreSQL:
"BaseCotacoes": "Server=localhost;Database=ExemplosEFCore2;Port=5432;User Id=postgres;Password=PostgreSQL2017!;"

Bloquear acesso de APIS com roles:
https://balta.io/blog/aspnet-core-autenticacao-autorizacao
https://stackoverflow.com/questions/49426781/jwt-authentication-by-role-claims-in-asp-net-core-identity

Publicando Projeto .NET CORE no IIS:
http://carloscds.net/2017/08/publicando-uma-aplicao-asp-net-core-no-iis/
https://medium.com/@alexalvess/publicando-aplica%C3%A7%C3%A3o-net-core-no-iss-f4079c2f312

Adicionando Log dos comandos do EF core:
https://www.entityframeworktutorial.net/efcore/logging-in-entityframework-core.aspx

LEMBRETES IMPORTANTES!!!

- Links sobre geração de Token e API com EF Core 3.0
https://balta.io/blog/aspnetcore-3-autenticacao-autorizacao-bearer-jwt
https://balta.io/blog/apis-data-driven-com-aspnet-core-3-e-ef-core-3-parte-1

- Comandos de instalação de packages necessarios para funcionamento do EF Core 3.0 (Rodar no package manager console)
Install-Package Microsoft.EntityFrameworkCore.InMemory
Install-Package Microsoft.AspNetCore.Authentication.JwtBearer -Version 3.1.0

- JSON para teste/geração do token via postman
{"username":"roberto","password":"roberto"}

- Dicas:
A tag [fromService] realiza a injeção de dependencia de forma automatica, sem necessidade de construtor.

-> Referência virtual faz que uma propriedade, objeto ou metodo possa ser substituido por uma dos tres argumentos que o herde.

RODAR SCRIPT SQL VIA MIGRATION!!!

-> Abrir o Package Manager Console
-> Rodar o comando Add-Migration "Nome do script SQL"
-> Assim que gerar o arquivo C# da migration, criar um arquivo com extensao ".SQL" dentro da pasta migrations.
-> Editar o metodo UP com um dos codigos abaixo

Ex:
protected override void Up(MigrationBuilder migrationBuilder)
{
   var schema = "starter_core";
   migrationBuilder.Sql($"INSERT INTO [{schema}].[Roles] ([Name]) VALUES ('transporter')");
}

OU 

public override void Up()
{
    string sqlResName = typeof(RunSqlScript).Namespace  + ".201801310940543_RunSqlScript.sql";
    this.SqlResource(sqlResName );
}

OU 

protected override void Up(MigrationBuilder migrationBuilder)
{
    var assembly = Assembly.GetExecutingAssembly();
    string resourceName = typeof(RunSqlScript).Namespace + ".20191220105024_RunSqlScript.sql";
    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
    {
    using (StreamReader reader = new StreamReader(stream))
    {
        string sqlResult = reader.ReadToEnd();
        migrationBuilder.Sql(sqlResult);
    }
}

Referência:
-> https://stackoverflow.com/questions/32125937/can-we-run-sql-script-using-code-first-migrations
-> https://stackoverflow.com/questions/45035754/how-to-run-migration-sql-script-using-entity-framework-core

EFETUAR RELACIONAMENTO ENTRE TABELAS DE FORMA MANUAL

No metodo OnModelCreating da classe WebSrvContext, os relacionamentos podem ser feitos da seguinte forma:
-> Exemplo de configurar relacionamento 1 para muitos
	modelBuilder.Entity<Student>()
    .HasOne<Grade>(s => s.Grade)
    .WithMany(g => g.Students)
    .HasForeignKey(s => s.CurrentGradeId);

-> Exemplo de configurar relacionamento 1 para 1
	modelBuilder.Entity<Student>()
    .HasOne<StudentAddress>(s => s.Address)
    .WithOne(ad => ad.Student)
    .HasForeignKey<StudentAddress>(ad => ad.AddressOfStudentId);

-> Exemplo de configurar relacionamento muitos para muitos
	
	modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.SId, sc.CId });

	modelBuilder.Entity<StudentCourse>()
    .HasOne<Student>(sc => sc.Student)
    .WithMany(s => s.StudentCourses)
    .HasForeignKey(sc => sc.SId);

	modelBuilder.Entity<StudentCourse>()
    .HasOne<Course>(sc => sc.Course)
    .WithMany(s => s.StudentCourses)
    .HasForeignKey(sc => sc.CId);

    * Aplicando Predicate em Campos de data e Hora (Tipo DateTime) 
  return p =>
                   (string.IsNullOrWhiteSpace(filter.User) || p.User.Name.Trim().ToUpper().Contains(filter.User.Trim().ToUpper()))
                   &&
                   ((!filter.BeginCreatedDateAt.HasValue && !filter.EndCreatedDateAt.HasValue) ||
                   (filter.BeginCreatedDateAt.HasValue && !filter.EndCreatedDateAt.HasValue && filter.BeginCreatedDateAt.Value.Date == p.CreatedAt.Date) ||
                   (!filter.BeginCreatedDateAt.HasValue && filter.EndCreatedDateAt.HasValue && filter.EndCreatedDateAt.Value.Date == p.CreatedAt.Date) ||
                   ((filter.BeginCreatedDateAt.HasValue && filter.EndCreatedDateAt.HasValue && (p.CreatedAt.Date >= filter.BeginCreatedDateAt.Value.Date && p.CreatedAt.Date <= filter.EndCreatedDateAt.Value.Date))))
                   &&
                   ((!filter.BeginCreatedTimeAt.HasValue && !filter.EndCreatedTimeAt.HasValue) ||
                   (filter.BeginCreatedTimeAt.HasValue && !filter.EndCreatedTimeAt.HasValue && filter.BeginCreatedTimeAt.Value.TimeOfDay == p.CreatedAt.TimeOfDay) ||
                   (!filter.BeginCreatedTimeAt.HasValue && filter.EndCreatedTimeAt.HasValue && filter.EndCreatedTimeAt.Value.TimeOfDay == p.CreatedAt.TimeOfDay) ||
                   ((filter.BeginCreatedTimeAt.HasValue && filter.EndCreatedTimeAt.HasValue && (p.CreatedAt.TimeOfDay >= filter.BeginCreatedTimeAt.Value.TimeOfDay && p.CreatedAt.TimeOfDay <= filter.EndCreatedTimeAt.Value.TimeOfDay))))
                   &&
                   (filter.Success == null || p.Success == filter.Success);

Obs: TimeStamp pode ser comparado direto sem necessidade do TimeOfDay

* Aplicando Predicate em Campos, onde tenha uma lista de MultiSelect no front
return p =>
                   (string.IsNullOrWhiteSpace(filter.City) || p.City.Name.Trim().ToUpper().Contains(filter.City.Trim().ToUpper()))
                   &&
                   (!filter.State.HasValue || p.City.State == filter.State)
                   &&
                   (!filter.ExternalCode.HasValue || filter.ExternalCode == p.ExternalCode)
                   &&
                   ((filter.IdConsortium == null || filter.IdConsortium.Count() == 0) || p.CityHallConsortiumSet.Any(x=> filter.IdConsortium.Contains(x.IdConsortium)));

-- Gerador de PDF
https://medium.com/@erikthiago/gerador-de-pdf-no-asp-net-core-e494650eb3c9 (Rotativa ou DinkToPdf)

-- Reciclagem C#

>> Membros static são acessados diretamente pela classe do tipo static
>> Partial class serve para dividir uma classe muito grande em 2 ou mais partes
>> Classe abstrata e um modelo de classe que pode ser herdada por outras classes, porem nao pode ser instanciada a partir de um objeto
>> Metodo abstract so fica a assinatura, sem necessidade de implementação
>> Metodo virtual tem implementação na classe abstrata, sem necessidade de ter sua assinatura nas outras classes
>> Classe sealed pode herdar outras classes, mas ninguem pode herdar a classe sealed

-- Autenticação e Autorização Bearer JWT
>> balta.io/artigos/aspnet-5-autenticacao-autorizacao-bearer-jwt

-- Implementando criptografia dos dados na base de dados, devido ao padrão LGPD
NET CORE 2.1 ou 3.1 >> https://entityframeworkcore.com/knowledge-base/50993914/implementing-encryption-in-entity-framework-model-classes
NET CORE 5.0 ou superior >> https://sd.blackball.lv/articles/read/18805
NET CORE 5.0 ou superior >> https://emrekizildas.medium.com/encrypt-your-database-columns-with-entityframework-1f129b19bdf8

>> A PARTIR DO EF CORE 5.0 E POSSIVEL FAZER O MAPEAMENTO DE UMA VIEW PARA DENTRO DE UMA ENTIDADE C#
exemplo: Dentro do metodo override OnModelCreating, fazemos o codigo abaixo:
modelBuilder.Entity<Classe>(x => { x.ToSqlQuery("SELECT * FROM VWTESTE")})

-- Passo para criação de um chat ou envio de notificações a aplicação com SignalR
>> https://www.c-sharpcorner.com/article/real-time-angular-11-application-with-signalr-and-net-5/

-- Gerar diagrama de classe 
>> Verificar se o pacote class designer está instalado, caso não esteja efetuar o passo abaixo:
    >> Menu Tools >> Get Tools and Features
    >> Na opção menu Individual components >> Selecionar a opção class designer

-- Novidades do NET 6 para trabalhar com lista
>> lista.firstordefault(x => x == 1, "0"); first ou single or default e possivel passar um valor padrao apos a query
>> Antes era assim...lista.firstordefault(x => x == 1) ?? 0;
>> MinBy => Pega o menor valor da lista
>> MaxBy => Pega o maior valor da lista
>> Chunk => Reparte a lista em varias sublistas conforme o numero de itens solicitado
>> Enumerable.range(1,500) => Cria uma lista de 1 ate 500
>> Zip => Voce compacta 1 ou mais listas em uma unica, fazendo uma unica execução
>> Take => Conseguimos passar uma margem de elementos take(25..50) ou pegar um valor especifico take(25)

-- Realizando Globalização e Localização de idioma
>> Adicionar o serviço abaixo na classe startup
services.AddLocalization()
>> Adicionar a aplicação abaixo na classe startup
var supportedCultures = new [] {"pt-BR", "en-US", "it"};
var localizationOptions = new RequestLocalizationOptions()
.SetDefaultCulture(supportedCultures[0])
.AddSupportedCultures(supportedCultures)
.AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);

>> Aplicando a funcionalidade na controller
Utilizar o data annotation [FromServices] IStringLocalizer<Messages> localizer
Para pegar o valor, basta utilizar o comando localizer[""].Value

>> Criar o Resource para que o StringLocalizer possa pegar o valor do idioma definido
Exemplo de nome de arquivo: Nomedoarquivo.idioma.resx (Criar um arquivo padrao sem o idioma especificado, somente Nomedoarquivo.resx)
Obs:
Configurar na propriedade do arquivo .resx
Custom Tool >> PublicResXFileCodeGenerator

-- Gerar excel EPPLUS ou NPOI
http://www.macoratti.net/21/03/c_xlsplus1.htm

-- Gerar excel sem biblioteca de terceiros
https://github.com/rsantosdev/aspnetcore-report-export-samples

-- Tratamento de erros com UseExceptionHandler
http://www.macoratti.net/21/04/aspc_errglobl1.htm

-- Trabalhando com SeriLog e Seq (Exibindo Log de erro em aplicação externa)
https://rafaelcruz.azurewebsites.net/2016/11/08/criando-log-estruturados-com-seq-e-serilog/
https://balta.io/blog/aspnet-serilog?utm_source=LinkedIn&utm_campaign=social-to-blog&utm_content=aspnet-serilog&utm_medium=social

-- Se precisar trabalhar com variaveis de ambiente ou Serilog, seguir modelo do Webnotes

-- Realizando BulkInsert para adicionar 1000 ou mais registros de forma mais otimizada do que com AddRange
https://macoratti.net.br/21/05/ef_bulkinsert1.htm

-- Criptografar parametros da URL como ID e outros campos...
http://www.macoratti.net/21/05/aspnc_urlprot1.htm