# Garage2.0 En Webbapplikation
Sammanbindning av .NET1, MVC, objektorientering, webbprogrammering
## Arbetssätt
Arbetet med Garaget kommer att ske i grupper om tre till fyra personer. tanken är att ni som grupp tillsammans ska diskutera tillvägagångssätt, uppbyggnad och utföring av projektet. Inom detta ingår Modellstruktur, databasstruktur, Controllers, och repositories som behövs och vilka views som kan tänkas behövas. Alltså tillverka en fullstack lösning. Uppdelningen av arbetet är helt upp till varje grupp, det är dock viktigt att samtliga i gruppen är medvetna om hur applikationen fungerar. Under redovisning kan specifika personer frågas om specifika delar av programmet, då är inte "Jag har inte programmerat den här delen av programmet" ett godkänt svar.

## Om applikationen
Applikationen skapas för att vidare implementera MVC struktur, och denna på någorlunda vis går att tolka utifrån ett bemannat garage.
Modellen är då fordon, smo tas emot i garaget. Dessa fordon måste förhålla sig till den definition av fordon som garage-företaget tidigare bestämt. Exempelvis kan det vara att det måste finnas reg-nr, färg, ha ett antal hjul och vara antingen en bil, lastbil, båt, eller motorcukel.
Controllern är receptionspersonalen, som har översigktlig information och ser till att arbetarna gör det som kunden (användaren) behöver. Det kan excempelvis handla om att dirigera kunden till rätt plats för att få sin bil inställd eller uthämtad.
Views är den visuella representationen. Alltså det som syns. Antingen översiktligt (att se in i ett garage för att snabbt se om det finns plats) eller mer specifikt (leta efter parkeringsplats eller efter specifik bil i detalj).
Repositories är då arbetarna i garaget som utför alla uppgifter enligt receptionisternas order.

## Krav på applikationen
- Modell
  - Det skall enbart finnas en modell för fordon
  - Owner, RegNr, Color, NumberOfWheels, och VehicleType är obligatoriska fält.
  - Fordonstyp skall existera som en property av typen ENUM i fordonsmodellen
- Databas
  - Skall hantera lagring av objekt enligt modellen fordon
- Controllers
  - Dirigera till olika vyer och kalla repositories
  - Kontrollera http statuskoder
  - Kontrollera modelstates
- Repositories
  - Skall hantera all funktionalitet vid:
    - Tilläggning av fordon
    - Borttagning av fordon
    - Sökning av fordon
    - Filtering av fordon
    - Övrig funktionalitet som läggs till
- Funktionalitetskrav
  - Användare skall kunna se en lista av samtliga fordon i garaget, grupperat efter fordonstyp och sorterat efter inställningstid.
  - Användare skall kunna lägga till fordon i Garaget
  - Användare skall kunna ta bort fordon ur Garaget
  - Användare ska kunna söka på fordon i Garaget
    - Baserat på RegNr
    - Baserat på Flera valbara properties
  - Använare skall kunna filtrera fordon i garaget
    - Baserat på fordonstyp
    - Baserat på Inställningsdatum (idag, denna vecka ...)
- Views
  - Det skall finnas views för att hantera och visa upp samtlig funktionalitet nämnd ovan
  - En view kan användas till fler än en funktion.
  - Det skall gå att navigera till samtliga views utan att redigera adressfältet.
## Utökad funktionalitet
Gör detta enbart om ni är färdiga med ovanstående
- Gör det möjligt att hyra en plats, alltså låta en bil "tas ut" ur garaget, utan att den ger upp sin plats i databasen
- Gör det möjligt att ändra sorteringen på tabeller genom att klicka på respektive kolumn-titel
- Gör det möjligt att redigera fordon som står i garaget, det vill säga ändra specifikationer
- Gör garaget till ett betal-garage med specifika priser beroende på vilket fordon som läggs in
- Gör det möjligt att ha en specifik parkeringstid (Exempelvis parkerad från nu till imorgon samma tid alternativt från imorgon till om två dagar
- Gör om betalsystemet till fordonstyp * Tid parkerad
  - Debitera vid utcheckning genom att presentera summan för användaren innan konfirmation
- Gör det möjligt att debitera extra för fordon som stått längre än sin parkeringstid med 2 * dygnstaxa för varje dygn över sin parkeringstid
Behövs fler uppgifter, kontakta lärare.
## Bra att ha länkar
- http://www.pluralsight.com/courses/mvc4-building
- http://www.pluralsight.com/courses/csharp-fundamentals-csharp5
- http://www.pluralsight.com/courses/csharp-collections
- https://msdn.microsoft.com/en-us/library/bb397926.aspx
- http://www.pluralsight.com/courses/linq-fundamentals
- http://www.pluralsight.com/courses/linq-data-access
- http://www.github.com
- http://www.stackoverflow.com
- http://www.google.com

Lycka till!
