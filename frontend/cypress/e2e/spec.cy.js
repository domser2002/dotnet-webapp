describe('End-to-End Test', () => {
  it('completes the entire process', () => {
    // Krok 1: Przejście na stronę /formPage po kliknięciu przycisku "Send Package"
    cy.visit('http://localhost:3000/');

    cy.wait(2000);  // Poczekaj na załadowanie strony

    cy.contains('Send Package').click();

    cy.wait(1000);  // Poczekaj na załadowanie strony

    cy.url().should('include', '/form');
    // Wypełnienie pól formularza
    cy.get('label:contains("Length") + div input').type('2');
    cy.get('label:contains("Width") + div input').type('3');
    cy.get('label:contains("Height") + div input').type('4');
    cy.get('label:contains("Weight") + div input').type('5');
    cy.get('label:contains("Source Street") + div input').eq(0).type('Prosta');
    cy.get('label:contains("Source Street Number") + div input').type('1');
    cy.get('label:contains("Source Flat Number") + div input').type('1');
    cy.get('label:contains("Source Postal Code") + div input').type('12-345');
    cy.get('label:contains("Source City") + div input').type('Tczow');
    cy.get('label:contains("Destination Street") + div input').eq(0).type('Krzywa');
    cy.get('label:contains("Destination Street Number") + div input').type('3');
    cy.get('label:contains("Destination Flat Number") + div input').type('3');
    cy.get('label:contains("Destination Postal Code") + div input').type('54-321');
    cy.get('label:contains("Destination City") + div input').type('Zwolen');
    cy.get('input[type="checkbox"]').check();
    cy.get('label:contains("Date from") + div input').clear().type('02012024');
    cy.get('label:contains("Date to") + div input').clear().type('02112024');

    // Kliknięcie przycisku "Submit"
    cy.get('button:contains("Submit")').click();
    // Po kliknięciu przycisku Submit, sprawdź, czy zostajesz przeniesiony na odpowiednią stronę
    cy.url().should('include', '/couriersList');

    // Poczekaj na załadowanie strony
    cy.wait(2000);  // Dostosuj czas oczekiwania według potrzeb

    // Kliknij przycisk z listy, zawierający tekst "NajlepszaFirma"
    cy.contains('Choose').click();

    cy.wait(1000);  // Poczekaj na załadowanie strony

    cy.url().should('include', '/contactInformation');

    // Wprowadź dane personalne
    cy.get('label:contains("Personal data") + div input').type('Mateusz Chmurzynski');

    // Wprowadź adres e-mail
    cy.get('label:contains("E-mail") + div input').type('mdp.ch3@gmail.com');

    // Wprowadź adres źródłowy
    cy.get('label:contains("Street") + div input').eq(0).type('Bartodzieje');
    cy.get('label:contains("Street Number") + div input').type('1');
    cy.get('label:contains("Flat Number") + div input').type('61');
    cy.get('label:contains("Postal Code") + div input').type('26-706');
    cy.get('label:contains("City") + div input').type('Tczow');

    // Kliknięcie przycisku "Submit"
    cy.get('button:contains("Submit")').click();
    cy.wait(2000);
    // Sprawdź, czy strona po zatwierdzeniu formularza to "/summaryPage"
    cy.url().should('include', '/summaryPage');

    // Kliknięcie przycisku "Submit"
    cy.get('button:contains("Submit request")').click();

    cy.url().should('include', '/');
  });
  
});
