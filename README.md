# AvisoUrgente
Segue os passos a serem executados para rodar os itens do teste.


  * Objetivo 1
  
    - Foi criado o banco de dedos utilizando o SQLite e o arquivo encontra-se dentro do diretório Api/ProjetoApi/Models/AvisoUrgente.db
    - Para criar o Banco de Dados utilizei a ferramenta DB Browser (SQLite), que pode ser baixada no endereço: https://sqlitebrowser.org/dl/
  
  * Objetivo 2
  
    - Fazer o clone da pasta Api
    - Abrir no VisualStudio ou outra IDE o arquivo ProjetoApi.sln
    - Compilar o arquivo no modo IIS, para que a aplicação seja criada dentro do IIS para poder poder atender outros requisitos do teste
    
    
  * Objetivo 3
  
    - Fazer o clone da pasta RaspagemTRF1
    - Abrir no VisualStudio ou outra IDE o arquivo RaspagemTRF1.sln
    - Para buscar as informações das movimentações do processos cadastrador a Api citada no Objetivo 2 deverá estar sendo executada no IIS local.
    - Foi utilizado o Tribunal Regional Federal da 1ª Região para realizar a busca dos processos.
    - Para cadastrar o movimentação dos processos cadastros basta compilar o projeto que o mesmo irá verificar se os processos tem alguma movimentação conforme a data e descrição, caso a movimentação já esteja cadastrada na base de dados local a mesma não é cadastrada novamente.
    
# Qualquer dúvida ou problema, favor entrar em contato (62) 99806-1918
    


