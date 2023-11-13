# TestTask
Честно сказать задание не доделал полность, не сделал docker образ для разворота, работа слишком объемная для тестового задания. Думаю, что это критично, в любом случае был рад попробовать попасть на собеседование в вашу компанию.
Командs для миграции бд: dotnet ef database update --startup-project WebApi/WebApi.csproj --project Persistence/TestTask.Persistence.csproj --context AppUserDbContext,
                         dotnet ef database update --startup-project WebApi/WebApi.csproj --project Persistence/TestTask.Persistence.csproj --context FileApplicationDbContext
