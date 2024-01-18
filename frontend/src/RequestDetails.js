import { useEffect, useState } from 'react';
import { Button, Typography} from '@mui/material';
import { useStore } from './store.js';

import './OfficeWorkerPanel.css';
import './CouriersListPage.css';


export function RequestDetails() {

    const [details, setDetails] = useState();
    const {RequestId} = useStore();

    useEffect(() => {
      const fetchData = async () => {
          try {
            const response = await fetch(`https://localhost:7160/api/requests/${RequestId}`);
            const data = await response.json();
            setDetails(data);
          } catch (error) {
              console.error('Error fetching user data:', error);
          }
      };
  
      fetchData();
  }, [RequestId]);


    const handleAcceptClick = async () => 
    {
      try {
        const response = await fetch(`https://localhost:7160/api/requests/${RequestId}`, {
          method: 'PATCH',
          headers: {
            'Content-Type': 'application/json',
          },
          body: {
            Status: 1,
        },
        });

        const data = await response.json();
        console.log('Pomyślnie zaktualizowano zasób:', data);

      } catch (error) {
        console.error('Błąd podczas aktualizacji zasobu:', error.message);
      }
    }

    const handleDeclineClick = async () => 
    {
      try {
        const response = await fetch(`https://localhost:7160/api/requests/${RequestId}`, {
          method: 'PATCH',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            Status: 5,
          }),
        });

        const data = await response.json();
        console.log('Pomyślnie zaktualizowano zasób:', data);

      } catch (error) {
        console.error('Błąd podczas aktualizacji zasobu:', error.message);
      }
    }
    return (
      <div className="Panel-header-officeWorker">

        <Typography variant="pre" className='gray-text'>
          {JSON.stringify(details, null, 2)}
        </Typography>

        <div>
        <Button
                variant="contained"
                color="primary"
                style={{ backgroundColor: '#0d10a6', color: 'white' }}
                onClick={() => handleAcceptClick()}
            >
                Accept
            </Button>
            <Button
                variant="contained"
                color="primary"
                style={{ backgroundColor: '#0d10a6', color: 'white' }}
                onClick={() => handleDeclineClick()}
            >
                Decline
            </Button>
        </div>

      </div>
    );
}
