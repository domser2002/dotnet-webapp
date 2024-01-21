import { useEffect, useState } from 'react';
import { Button, Typography, Box, TextField} from '@mui/material';
import { useStore } from './store.js';
import { useAuth0 } from '@auth0/auth0-react';
import './OfficeWorkerPanel.css';
import './CouriersListPage.css';


export function RequestDetails() {

    const baseUrl = process.env.REACT_APP_API_URL;
    const apiRequests = baseUrl+"/api/requests";
    const apiRequestsAgreement = baseUrl+"/api/requests/agreement";
    const apiRequestsReceipt = baseUrl+"/api/requests/receipt";

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



    const [selectedAgreement, setSelectedAgreement] = useState(null);
    const [selectedReceipt, setSelectedReceipt] = useState(null);
    const handleAgreementChange = (event) => {
      const file = event.target.files[0];
      setSelectedAgreement(file);
    };

    const handleReceiptChange = (event) => {
      const file = event.target.files[0];
      setSelectedReceipt(file);
    };

    // const handleSubmit = (event) => {
    //   event.preventDefault();
    //   // Tutaj możesz przetworzyć lub przesłać plik do serwera
    //   if (selectedAgreement) {
    //     console.log('Agreement:', selectedAgreement);
    //     console.log('Receipt:', selectedReceipt);
    //     // Dodaj kod obsługi przesyłania pliku tutaj
    //     // TUTAJ WYSŁAĆ PLIK
    //     // HANDLE ACCEPT
    //   }
    // };


    const handleAcceptClick = async () => {
      try {
        const claims = await getIdTokenClaims();
        const formDataAgreement = new FormData();
        const formDataReceipt = new FormData();

        // Dodaj plik umowy do formularza
        if (selectedAgreement) {
          formDataAgreement.append('agreement', selectedAgreement);
        }
    
        // Dodaj plik paragonu do formularza
        if (selectedReceipt) {
          formDataReceipt.append('receipt', selectedReceipt);
        }
    
        const responseAgreement = await fetch(`${apiRequestsAgreement}/${RequestId}`, {
          method: 'POST',
          headers: {
            Authorization: `Bearer ${claims["__raw"]}`,
          },
          body: formDataAgreement,
        });
        // Dodaj inne dane do formularza (jeśli są wymagane)
        //formData.append('Status', 1);
    
        const response = await fetch(`${apiRequestsReceipt}/${RequestId}`, {
          method: 'POST',
          headers: {
            Authorization: `Bearer ${claims["__raw"]}`,
          },
          body: formDataReceipt,
        });
    
        const responsePatch = await fetch(`${apiRequests}/${RequestId}`, {
          method: 'PATCH',
          headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${claims["__raw"]}`,
          },
          body: JSON.stringify({
            Status: 1,
        }),
        });


        const data = await response.json();
        console.log('Pomyślnie zaktualizowano zasób:', data);
      } catch (error) {
        console.error('Błąd podczas aktualizacji zasobu:', error.message);
      }
    };

    // const handleAcceptClick = async () => 
    // {
    //   try {
    //     const claims = await getIdTokenClaims();
    //     const response = await fetch(`${apiRequests}/${RequestId}`, {
    //       method: 'PATCH',
    //       headers: {
    //         'Content-Type': 'application/json',
    //         Authorization: `Bearer ${claims["__raw"]}`,
    //       },
    //       body: JSON.stringify({
    //         Status: 1,
    //     }),
    //     });

    //     const data = await response.json();
    //     console.log('Pomyślnie zaktualizowano zasób:', data);

    //   } catch (error) {
    //     console.error('Błąd podczas aktualizacji zasobu:', error.message);
    //   }
    // }

    // WYSLAC EMAIL Z ODRZUCENIEM
    const handleDeclineClick = async () => 
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
    return (
      <div className="Panel-header-officeWorker">
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

          {/* owner*/}
          <Typography variant="body1" gutterBottom className="gray-text">
            Owner: Personal Data: {details.owner.personalData}, Email: {details.owner.email}, Address: Street: {details.owner.address.street}, Street Number: {details.owner.address.streetNumber}, Flat Number: {details.owner.address.flatNumber}, Postal Code: {details.owner.address.postalCode}, City: {details.owner.address.city}, ID: {details.owner.id}
          </Typography>

          {/* id */}
          <Typography variant="body1" gutterBottom className="gray-text">
            ID: {details.id}
          </Typography>
        </Box>
        
      )}
      <div className='button-container'>

              <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
              <Typography variant="body1" gutterBottom className="gray-text">
                Agreement file:
              </Typography>
                <TextField
                  type="file"
                  accept=".pdf"
                  onChange={handleAgreementChange}
                  variant="outlined"
                />
              <Typography variant="body1" gutterBottom className="gray-text">
                Receipt file:
              </Typography>
                <TextField
                  type="file"
                  accept=".pdf"
                  onChange={handleReceiptChange}
                  variant="outlined"
                />
                <Button
                  type="button"
                  variant="contained"
                  color="primary"
                  style={{ backgroundColor: '#0d10a6', color: 'white' }}
                  onClick={() => handleAcceptClick()}
                  disabled={!selectedAgreement || !selectedReceipt}
                >
                  Accept
                </Button>
              </Box>


        <div>
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
    </div>
    );
}
