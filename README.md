# API de Armazenamento (Storage API)

Esta é uma API para upload, download e listagem de arquivos, semelhante ao Amazon S3. Ela foi desenvolvida usando C# e ASP.NET Core.

## Endpoints

### 1. **Upload de arquivo**

- **URL:** `POST /api/storage/upload`
- **Descrição:** Faz o upload de um arquivo para o servidor.
- **Parâmetros:**
  - `file` (arquivo): O arquivo a ser enviado. Este arquivo será enviado como dados binários.
  
- **Respostas:**
  - `200 OK`: Arquivo carregado com sucesso. Retorna a mensagem e o caminho do arquivo.
  - `400 BadRequest`: Se o arquivo for vazio ou exceder o tamanho máximo permitido.
  
- **Exemplo de Requisição (cURL):**
  ```bash
  curl -X 'POST' \
    'http://localhost:5097/api/storage/upload' \
    -H 'accept: */*' \
    -H 'Content-Type: multipart/form-data' \
    -F 'file=@path/to/your/file.jpg'

### 2. **Acessar o arquivo carregado**
- **URL para acessar o arquivo:** Após o upload do arquivo, ele será armazenado na pasta wwwroot e pode ser acessado diretamente através do caminho da URL. Não é necessário um endpoint, basta acessar o arquivo diretamente pela URL.
- **Exemplo de URL para acessar o arquivo:** http://localhost:5097/{filePath} onde filePath é o caminho retornado no endpoint de upload do arquivo.
