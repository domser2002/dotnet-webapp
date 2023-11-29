//import axios from 'axios';
import { useState } from 'react';
//const url = 'https://localhost:7160/api/offers';

export function CouriersListPage() {

    const [offers, setOffers] = useState();
    fetch('https://localhost:7160/api/offers').then(response => response.json()).then(data => {setOffers(data);})
    .catch(error => {
      console.error('GET error:', error);
    });

    return (
    <div>
        <pre>
            {JSON.stringify(offers, null, 2)}
        </pre>
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
