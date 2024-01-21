import { useEffect, useState } from 'react';
import { Button, Typography, Box, TextField} from '@mui/material';
import { useStore } from './store.js';
import { useAuth0 } from '@auth0/auth0-react';
import { useNavigate } from 'react-router-dom';

import './CourierPanel.css';
import './CouriersListPage.css';


export function CourierRequestDetails() {

    const baseUrl = process.env.REACT_APP_API_URL;
    const requestsId = baseUrl+"/api/requests";

    const [courierMessage, setCourierMessage] = useState("");
  

    const [details, setDetails] = useState();
    const {RequestId} = useStore();
    const {getIdTokenClaims} = useAuth0();
    const navigate = useNavigate();

    useEffect(() => {
      const fetchData = async () => {
          try {
            const claims = await getIdTokenClaims();
            const response = await fetch(`${requestsId}/${RequestId}`, {
              headers: {
                  Authorization: `Bearer ${claims["__raw"]}`,
                  // Dodaj inne nagłówki, jeśli są potrzebne
              },
          });
            const data = await response.json();
            setDetails(data);
          } catch (error) {
              console.error('Error fetching user data:', error);
          }
      };
  
      fetchData();
  }, [RequestId, requestsId, getIdTokenClaims]);



    const handleReceivedClick = async () => 
    {
      try {
        const claims = await getIdTokenClaims();
        const response = await fetch(`${requestsId}/${RequestId}`, {
          method: 'PATCH',
          headers: {
            'Content-Type': 'application/json',
             Authorization: `Bearer ${claims["__raw"]}`,
          },
          body: JSON.stringify({
            Status: 2,
          }),
        });

        const data = await response.json();
        console.log('Pomyślnie zaktualizowano zasób:', data);
        navigate("/courierPanel");
      } catch (error) {
        console.error('Błąd podczas aktualizacji zasobu:', error.message);
      }
    }

    const handleDeliveredClick = async () => 
    {
      try {
        const claims = await getIdTokenClaims();
        const response = await fetch(`${requestsId}/${RequestId}`, {
          method: 'PATCH',
          headers: {
            'Content-Type': 'application/json',
             Authorization: `Bearer ${claims["__raw"]}`,
          },
          body: JSON.stringify({
            Status: 3,
          }),
        });

        const data = await response.json();
        console.log('Pomyślnie zaktualizowano zasób:', data);
        navigate("/courierPanel");
      } catch (error) {
        console.error('Błąd podczas aktualizacji zasobu:', error.message);
      }
    }

    const handleCannotDeliverClick = async () => 
    {
      try {
        const claims = await getIdTokenClaims();
        const courierId = claims["sub"].split('|')[1];
        const response = await fetch(`${requestsId}/${RequestId}`, {
          method: 'PATCH',
          headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${claims["__raw"]}`,
          },
          body: JSON.stringify({
            Status: 4,
            Message: courierMessage,
            CourierId: courierId,
          }),
        });
        navigate("/courierPanel");
        const data = await response.json();
        console.log('Pomyślnie zaktualizowano zasób:', data);

      } catch (error) {
        console.error('Błąd podczas aktualizacji zasobu:', error.message);
      }
    }

    const getStatusText = (status) => {
      switch (status) {
        case 0:
          return 'Pending';
        case 1:
          return 'Accepted';
        case 2:
          return 'Received';
        case 3:
          return 'Delivered';
        case 4:
          return 'Cannot Deliver';
        case 5:
          return 'Cancelled';
        default:
          return 'Unknown Status';
      }
    };


    return (
      <div className="Panel-header-courier">
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
            Status: {getStatusText(details.status)}
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
          <div>
            <Button
                variant="contained"
                color="primary"
                style={{ backgroundColor: '#0d10a6', color: 'white' }}
                onClick={() => handleReceivedClick()}
              >Received
              </Button>
          </div>
            
          <div>
            <Button
                variant="contained"
                color="primary"
                style={{ backgroundColor: '#0d10a6', color: 'white' }}
                onClick={() => handleDeliveredClick()}
            >Delivered
            </Button>
          </div>
            

          <div className='button-message-container'>
            <TextField
              id="courier-message"
              label="Courier message"
              variant="outlined"
              multiline
              rows={4}
              value={courierMessage}
              onChange={(e) => setCourierMessage(e.target.value)}
            />
            <Button
              variant="contained"
              color="primary"
              style={{ backgroundColor: '#0d10a6', color: 'white' }}
              onClick={() => handleCannotDeliverClick()}
            >Cannot deliver
            </Button>
          </div>
      </div>
    </div>
    );
}
