# Service-Based-Architecture
**Systemet er et bibliotek med tre services (BookService, MemberService og LoanService).**


## Start services: 
- dotnet run --urls http://localhost:5001/ i BookService
- dotnet run --urls http://localhost:5002/ i MemberService
- dotnet run --urls http://localhost:5003/ i LoanService

*Hver service startes i sin egen mappe, ellers finder den ikke ../Data/.(hvor datafilerne er)*


## Data: 
books.json og members.json ligger i Data, og loans.json skal starte som [] (altså tom).

## Test det ud: 
- GET http://localhost:5001/books/1
- POST http://localhost:5003/loans?bookId=1&memberId=1 med curl.exe. *(åben en ny git bash inde i loan service og sæt dette ind)*
- I kan også teste statuskoderne ud (200 OK, 404 Not Found og 503 Service Unavailable) med fx. **curl.exe -i -X POST "http://localhost:5003/loans?bookId=1&memberId=1"**

## Frontend: 
bar åbn Frontend/bibliotek-frontend.html i en browser, når alle services kører.

## Kendte begrænsninger: 
ingen kontrol af, om en bog allerede er udlånt (bevidst, fordi det er forretningslogik + det ville tage længere tid), og HttpClient oprettes inde i endpointet (så vær sikker på, at i har alle tre services kørende).
