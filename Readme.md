# Trainee Management API

## Technology Used 

## How to Run 
1. Clone Github Repository
`https://github.com/snair1903/TraineeManagement.api.git`

2. Navigate to Folder 
`cd TraineeManagement.api`

3. Install Dependencies
`dotnet restore`

4. Change Connection String
```json
  "ConnectionStrings": {
    "DefaultConnection":
    "server=localhost;database=TraineeManagementDb;user=root;password=Snorlax@1010;"

  }
```

5. Database update
`dotnet ef database update`


6. Start the project
`dotnet run `

## API List 
### Trainee:
|Methods|Endpoints|
|:---|:---|
|GET|/api/health|
|GET|/api/trainee|
|GET|/api/trainee/{Id}|
|POST|/api/trainee|
|PUT|/api/trainee/{Id}|
|DELETE|/api/trainee/{Id}|
|GET|/api/trainee?search={query}|

## Sample Request JSON and Response JSON 

1. `GET` `/api/health`
Request:
```bash
curl -X 'GET' \
  'https://localhost:7206/api/health' \
  -H 'accept: */*'
  ```

  Response:
  ```bash
  {
  "status": "running",
  "application": "Trainee Management API",
  "timestamp": "2026-06-08T12:56:40"
}
  ```

  2. `GET` `/api/trainee`
Request:
```bash
curl -X 'GET' \
  'https://localhost:7206/api/trainee' \
  -H 'accept: */*'
  ```

  Response:
  ```bash
  [
  {
    "id": 1,
    "firstName": "string",
    "lastName": "string",
    "email": "string@gmail",
    "techStack": "string",
    "status": "",
    "createdDate": "2026-06-08T16:10:31.6517283+05:30",
    "updatedDate": "2026-06-08T16:10:31.6517736+05:30"
  },
  {
    "id": 2,
    "firstName": "string6",
    "lastName": "string",
    "email": "string@gmail",
    "techStack": "string",
    "status": "Active",
    "createdDate": "2026-06-08T16:10:35.2235989+05:30",
    "updatedDate": "2026-06-08T16:14:10.8199147+05:30"
  },
  {
    "id": 3,
    "firstName": "string1",
    "lastName": "string",
    "email": "string@gmail",
    "techStack": "string",
    "status": "",
    "createdDate": "2026-06-08T16:10:45.5810991+05:30",
    "updatedDate": "2026-06-08T16:10:45.5810992+05:30"
  }
]
  ```

  3. `POST` `/api/trainee`
Request:
```bash
curl -X 'POST' \
  'https://localhost:7206/api/trainee' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "firstName": "string2",
  "lastName": "string",
  "email": "string@gmail",
  "techStack": "string",
  "status": "Active"
}'
  ```

  Response:
  ```bash
  {
  "status": "running",
  "application": "Trainee Management API",
  "timestamp": "2026-06-08T12:56:40"
}
  ```

  4. `GET` `/api/trainee/{Id}`
Request:
```bash
curl -X 'GET' \
  'https://localhost:7206/api/trainee/2' \
  -H 'accept: */*'
  ```

  Response:
  ```bash
  {
  "id": 2,
  "firstName": "string6",
  "lastName": "string",
  "email": "string@gmail",
  "techStack": "string",
  "status": "Active",
  "createdDate": "2026-06-08T16:10:35.2235989+05:30",
  "updatedDate": "2026-06-08T16:14:10.8199147+05:30"
}
  ```

  5. `PUT` `/api/trainee/{Id}`
Request:
```bash
curl -X 'PUT' \
  'https://localhost:7206/api/trainee/2' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "firstName": "string6",
  "lastName": "string",
  "email": "string@gmail",
  "techStack": "string",
  "status": "Active"
}'
  ```

  Response:
  ```bash
  {
  "id": 2,
  "firstName": "string6",
  "lastName": "string",
  "email": "string@gmail",
  "techStack": "string",
  "status": "Active",
  "createdDate": "2026-06-08T16:10:35.2235989+05:30",
  "updatedDate": "2026-06-08T16:14:10.8199147+05:30"
}
  ```

  6. `DELETE` `/api/trainee/{Id}`
Request:
```bash
    curl -X 'DELETE' \
  'https://localhost:7206/api/trainee/4' \
  -H 'accept: */*'
  ```
  Response:
  status code:204.

  7. `GET` `api/trainee?search={query}`
  Request:
  ```bash
  curl -X 'GET' \
  'https://localhost:7206/api/trainee?search=string6' \
  -H 'accept: */*'
  ```
  Response:
  ```bash
  [
  {
    "id": 3,
    "firstName": "string6",
    "lastName": "string",
    "email": "string@gmail",
    "techStack": "string",
    "status": "",
    "createdDate": "2026-06-08T18:55:39.3565506+05:30",
    "updatedDate": "2026-06-08T18:55:39.3565508+05:30"
  }
]
  ```

