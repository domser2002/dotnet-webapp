import { useEffect, useState } from 'react';
import { Button, Typography, Box} from '@mui/material';
import { useStore } from './store.js';
import { useAuth0 } from '@auth0/auth0-react';

import './UserRequestDetails.css';
import './CouriersListPage.css';

export function UserRequestDetails() {

    const baseUrl = process.env.REACT_APP_API_URL;
    const apiRequests = baseUrl+"/api/requests";

    const [details, setDetails] = useState();
    const {RequestId} = useStore();

    const {getIdTokenClaims} = useAuth0();

    useEffect(() => {
      const fetchData = async () => {
          try {
            const claims = await getIdTokenClaims();
            const response = await fetch(`${apiRequests}/${RequestId}`, {
              headers: {
                  Authorization: `Bearer ${claims["__raw"]}`,

              },
          });
            const data = await response.json();
            setDetails(data);
          } catch (error) {
              console.error('Error fetching user data:', error);
          }
      };
  
      fetchData();
  }, [RequestId, apiRequests, getIdTokenClaims]);



    const handleChangeClick = async () => 
    {
      try {
        const claims = await getIdTokenClaims();
        const response = await fetch(`${apiRequests}/${RequestId}`, {
          method: 'PATCH',
          headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${claims["__raw"]}`,
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

    const isCancelDisabled = () => {
        if (details && details.cancelDate) {
          const currentDate = new Date();
          const cancelDate = new Date(details.cancelDate);
    
          return currentDate > cancelDate;
        }
        return false; // Jeśli brak cancelDate lub błąd w danych
      };

    return (
      <div className="userRequest-container">
        {details && (
        <Box>
          {/* package */}
          <Typography variant="body1" gutterBottom className="gray-text">
            Package: Length: {details.package.length}, Width: {details.package.width}, Height: {details.package.height}, Weight: {details.package.weight}, ID: {details.package.id}
          </Typography>
          
          {/* sourceAddress */}
          <Typography variant="body1" gutterBottom className="gray-text">
            Source Address: Street: {details.sourceAddress.street}, Street Number: {details.sourceAddress.streetNumber}, Flat Number: {details.sourceAddress.flatNumber}, Postal Code: {details.sourceAddress.postalCode}, City: {details.sourceAddress.city}
          </Typography>

          {/* destinationAddress */}
          <Typography variant="body1" gutterBottom className="gray-text">
            Destination Address: Street: {details.destinationAddress.street}, Street Number: {details.destinationAddress.streetNumber}, Flat Number: {details.destinationAddress.flatNumber}, Postal Code: {details.destinationAddress.postalCode}, City: {details.destinationAddress.city}
          </Typography>

          {/* pickupDate */}
          <Typography variant="body1" gutterBottom className="gray-text">
            Pickup Date: {details.pickupDate}
          </Typography>

          {/* deliveryDate */}
          <Typography variant="body1" gutterBottom className="gray-text">
            Delivery Date: {details.deliveryDate}
          </Typography>

          {/* cancelDate */}
          <Typography variant="body1" gutterBottom className="gray-text">
            Cancel Date: {details.cancelDate || 'N/A'}
          </Typography>

          {/* companyName */}
          <Typography variant="body1" gutterBottom className="gray-text">
            Company Name: {details.companyName}
          </Typography>

          {/* price */}
          <Typography variant="body1" gutterBottom className="gray-text">
            Price: {details.price}
          </Typography>

          {/* status */}
          <Typography variant="body1" gutterBottom className="gray-text">
            Status: {details.status}
          </Typography>

          {/* owner */}
          <Typography variant="body1" gutterBottom className="gray-text">
            Owner: Personal Data: {details.owner.personalData}, Email: {details.owner.email}, Address: Street: {details.owner.address.street}, Street Number: {details.owner.address.streetNumber}, Flat Number: {details.owner.address.flatNumber}, Postal Code: {details.owner.address.postalCode}, City: {details.owner.address.city}, ID: {details.owner.id}
          </Typography>

          {/* id */}
          <Typography variant="body1" gutterBottom className="gray-text">
            ID: {details.id}
          </Typography>
        </Box>
        
      )}
        <Typography variant="h5" gutterBottom className="gray-text">
            Change delivery status
        </Typography>
      <div className='button-container'>

            <Button
                variant="contained"
                color="primary"
                style={{ backgroundColor: '#0d10a6', color: 'white' }}
                onClick={() => handleChangeClick()}
                disabled={isCancelDisabled()}
            >Cancel Order
            </Button>
      </div>
    </div>
    );
}
