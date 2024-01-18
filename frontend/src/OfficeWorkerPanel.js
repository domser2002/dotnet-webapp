import { useEffect, useState } from 'react';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText'
import { Button} from '@mui/material';
import { useAuth0 } from '@auth0/auth0-react';
import { useNavigate } from 'react-router-dom';
import { useStore } from './store';

import './CouriersListPage.css';
import './OfficeWorkerPanel.css';


export function OfficeWorkerPanel() {

    const [requests, setRequests] = useState([]);
    const [ setCompany ] = useState();
    const {getIdTokenClaims} = useAuth0();
    const navigate = useNavigate();
    const {setRequestId} = useStore();
    useEffect(() => {
      const fetchUserData = async () => {
          try {
              const claims = await getIdTokenClaims();
              console.log(claims);
              const id = claims["sub"].split('|')[1];
              console.log(id);
              const response = await fetch(`https://localhost:7160/api/users/subs/${id}`);
              const data = await response.json();
              return data["companyName"];
              // setCompany(data["companyName"]);
              // console.log(data["companyName"]);
          } catch (error) {
              console.error('Error fetching user data:', error);
          }
      };
  
      const fetchRequestsData = async (d) => {
          try {
              const response = await fetch(`https://localhost:7160/companies/${d}`);
              const data = await response.json();
              setRequests(data);
          } catch (error) {
              console.error('Error fetching requests data:', error);
          }
      };
  
      const fetchData = async () => {
          const d = await fetchUserData();
          await fetchRequestsData(d);
      };
  
      fetchData();
  }, [setCompany, setRequests, getIdTokenClaims]);


    const handleDetailsClick = async (id) => 
    {
      setRequestId(id);
      navigate("./requestDetails");
    }


    return (
      <div className="Panel-header-officeWorker">

          <List>
            {requests.map((offer) => (
              <ListItem key={offer.id} className="list-item">
                <ListItemText
                  primary={offer.companyName}
                  secondary={`Price: ${offer.price}`}
                  className="list-item-text"
                />
                <Button
                  variant="contained"
                  color="primary"
                  className="list-item-button"
                  onClick={() => handleDetailsClick(offer.id)}
                >
                  Details
                </Button>

              </ListItem>
            ))}
          </List>

      </div>
    );

    // return (
    //   <div className="Panel-header-officeWorker">

    //       <List>
    //         {requests.map((offer) => (
    //           <ListItem key={offer.id} className="list-item">
    //             <ListItemText
    //               primary={offer.companyName}
    //               secondary={`Price: ${offer.price}`}
    //             />
    //             <Button
    //               variant="contained"
    //               color="primary"
    //               style={{ backgroundColor: '#0d10a6', color: 'white' }}
    //               onClick={() => handleAcceptClick(offer.id)}
    //             >
    //               Accept
    //             </Button>
    //             <Button
    //               variant="contained"
    //               color="primary"
    //               style={{ backgroundColor: '#0d10a6', color: 'white' }}
    //               onClick={() => handleDeclineClick(offer.id)}
    //             >
    //               Decline
    //             </Button>
    //           </ListItem>
    //         ))}
    //       </List>

    //   </div>
    // );
}