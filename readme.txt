
# ReDoMeAPI
1. Развертывание API
2. Скопировать содержимое папки bin\debug на машину
3. При необходимости создать базу с помощью скрипта Database\crebas.sql O(СУБД MS SQL Server Express 2008 r2 и позднее)
4. В файле ReDoMeAPI.exe.config указать параметры:
  - WebServicePort - номер tcp порта
  - Mode - https
  - ConnectionString  -строк подклбючения к SQL Server
5. Установить сертификат на машину
6. Выполнить netsh http add urlacl url=https://+:НОМЕР_ПОРТА/ user=YOUR_USERNAME 
7. Выполнить netsh http add sslcert ipport=0.0.0.0:НОМЕР_ПОРТА certhash=YOUR_THUMBPRINT_WITHOUT_SPACES appid={06aabebd-3a91-4b80-8a15-adfd3c8a0b14} 
8. Запустить ReDoMeAPI.exe