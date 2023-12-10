//import axios from 'axios';
import { useEffect, useState } from 'react';
//const url = 'https://localhost:7160/api/offers';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText'
import './CouriersListPage.css';
import { Button } from '@mui/material';
//import Typography from '@mui/material/Typography';

export function CouriersListPage() {

    const [offers, setOffers] = useState([]);

    useEffect(() => {
        fetch('https://localhost:7160/api/offers').then(response => response.json()).then(data => {setOffers(data);})
        .catch(error => {
        console.error('GET error:', error);
        });
    }, []);

    const handleButtonClick = async (id) => 
    {
      try {
        const response = await fetch(`https://localhost:7160/api/offers/${id}`, {
          method: 'PATCH',
          headers: {
            'Content-Type': 'application/json',
          },
          body: null,
        });

        if (!response.ok) {
          throw new Error('Network response was not ok');
        }

        const data = await response.json();
        console.log('Pomyślnie zaktualizowano zasób:', data);
        // Tutaj możesz zaktualizować lokalny stan komponentu lub podjąć inne akcje w zależności od potrzeb
      } catch (error) {
        console.error('Błąd podczas aktualizacji zasobu:', error.message);
      }
    }


    return (
    <div className="CouriersListPage-header">
      <List>
        {offers.map((offer) => (
          <ListItem key={offer.id}>
            <ListItemText
              primary={offer.companyName} 
              secondary={`Price: ${offer.price}`} 
            />
            <Button
              variant="contained"
              color="primary"
              style={{ backgroundColor: '#0d10a6', color: 'white' }}
              onClick={() => handleButtonClick(offer.id)}
            >Choose
            </Button>
          </ListItem>
        ))}
      </List>

    </div>
    );
}
// import React, { useEffect, useState } from 'react';

// export const CouriersListPage = () => {
//   const [data, setData] = useState(null);

//   useEffect(() => {
//     const fetchData = async () => {
//       try {
//         const response = await fetch('https://localhost:7160/api/offers', {
//           method: 'GET',
//           headers: {
//             'Content-Type': 'application/json', // Jeśli to wymagane
//             // Dodaj inne nagłówki, jeśli są wymagane
//           },
//           // Dodaj ciało żądania, jeśli to konieczne (choć w przypadku GET, zazwyczaj nie jest)
//         });

//         if (!response.ok) {
//           throw new Error('Network response was not ok');
//         }

//         const result = await response.json();
//         setData(result);
//       } catch (error) {
//         console.error('There was a problem with the fetch operation:', error);
//       }
//     };

//     fetchData();
//   }, []); // Pusta tablica oznacza, że useEffect wykona się tylko raz po zamontowaniu komponentu

//   return (
//     <div>
//       {/* Wyświetl dane, na przykład: */}
//       {data && (
//         <ul>
//           {data.map(item => (
//             <li key={item.id}>{item.name}</li>
//           ))}
//         </ul>
//       )}
//     </div>
//   );
// };
