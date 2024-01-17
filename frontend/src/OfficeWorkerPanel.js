import { useEffect, useState } from 'react';
//const url = 'https://localhost:7160/api/offers';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText'
import './CouriersListPage.css';
import { Button } from '@mui/material';
import { useAuth0 } from '@auth0/auth0-react';

export function OfficeWorkerPanel() {

    const [requests, setRequests] = useState([]);
    const [company, setCompany] = useState();
    const {user, getIdTokenClaims} = useAuth0();

    useEffect(() => {
        const fetchData = async () => {
            try {
                const claims = await getIdTokenClaims();
                console.log(claims);
                const response1 = await fetch(`https://localhost:7160/api/users/subs/${claims["sub"].split('|')[1]}`);
                const data1 = await response1.json();
                setCompany(data1["CompanyName"]);

    
                const response2 = await fetch(`https://localhost:7160/api/requests/companies/${company}`);
                const data2 = await response2.json();
                setRequests(data2);
            } catch (error) {
                console.error('Error in useEffect:', error);
            }
        };
    
        fetchData();
    }, [user.sub]);
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



        const data = await response.json();
        console.log('Pomyślnie zaktualizowano zasób:', data);
        // Tutaj możesz zaktualizować lokalny stan komponentu lub podjąć inne akcje w zależności od potrzeb
      } catch (error) {
        console.error('Błąd podczas aktualizacji zasobu:', error.message);
      }
    }

    return (
    <div className="App-header-officeWorker">
      <List>
        {requests.map((offer) => (
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
            >Accept
            </Button>
            <Button
              variant="contained"
              color="primary"
              style={{ backgroundColor: '#0d10a6', color: 'white' }}
              onClick={() => handleButtonClick(offer.id)}
            >Decline
            </Button>
          </ListItem>
        ))}
      </List>

    </div>
    );
}