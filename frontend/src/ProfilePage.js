import { useAuth0 } from "@auth0/auth0-react";
import { useState, useEffect } from "react";
import { Box, Paper, Typography} from "@mui/material";
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText'
import { Button, TextField, MenuItem} from '@mui/material';
import { useNavigate } from "react-router-dom";
import { useStore } from "./store";

import './App.css';
import './ProfilePage.css';

export const ProfilePage = () =>
{

    const baseUrl = process.env.REACT_APP_API_URL;
    const apiUsersSubsId = baseUrl+"/api/users/subs";


    const { isAuthenticated, getIdTokenClaims } = useAuth0();
    const [profile, setProfile] = useState();
    const navigate = useNavigate();

    const [role, setRole] = useState();

    const [requests, setRequests] = useState([]);

    const {setRequestId} = useStore();


    useEffect(() => {
        const getRolesFromToken = async () => {
          try {
            if (isAuthenticated) {
              const claims = await getIdTokenClaims();
    
              setRole(claims["role"][0]);
            }
          } catch (error) {
            console.error('Error while decoding token:', error);
          }
        };
    
        const fetchUserData = async () => {
          try {
            if (isAuthenticated) {
                const claims = await getIdTokenClaims();
                const id = claims["sub"].split('|')[1]
                const response = await fetch(`${apiUsersSubsId}/${id}`, {
                  headers: {
                      Authorization: `Bearer ${claims["__raw"]}`,
                  },
              });
                const data = await response.json();
                setProfile(data);
                console.log(data["requests"]);
                setRequests(data["requests"]);

              if (response.ok) {
                const data = await response.json();
                setProfile(data); // Assuming user data is in the response
              } else {
                console.error('Error while fetching user data:', response.statusText);
                setProfile(data);
              }
            }
          } catch (error) {
            console.error('Error while fetching user data:', error);
          }
        };

        // const fetchRequestsData = async (d) => {
        //   try {
        //     const claims = await getIdTokenClaims();
        //     const id = claims["sub"].split('|')[1]
        //       const response = await fetch(`https://localhost:7160/subs/${id}`);
        //       const data = await response.json();
        //       setRequests(data);
        //   } catch (error) {
        //       console.error('Error fetching requests data:', error);
        //   }
      //};
        getRolesFromToken();
        fetchUserData();
        //fetchRequestsData()
      }, [getIdTokenClaims, isAuthenticated, setProfile, apiUsersSubsId]);

  
      const handleDetailsClick = async (id) => 
      {
        setRequestId(id);
        navigate("./userRequestDetails");
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
  
      const [sortBy, setSortBy] = useState(null);
  
    const handleSortBy = (sortField) => {
      if (sortBy === sortField) {
        // Jeśli kliknięty guzik sortowania jest już aktywny, zmień kierunek sortowania
        setRequests([...requests.reverse()]);
      } else {
        // Sortuj po wybranym polu
        let sortedRequests = [...requests];
        switch (sortField) {
          case 'pickupDate':
            sortedRequests.sort((a, b) => new Date(a.pickupDate) - new Date(b.pickupDate));
            break;
          case 'deliveryDate':
            sortedRequests.sort((a, b) => new Date(a.deliveryDate) - new Date(b.deliveryDate));
            break;
          case 'status':
            sortedRequests.sort((a, b) => a.status - b.status);
            break;
          default:
            break;
        }
        setRequests(sortedRequests);
      }
      setSortBy(sortField);
    };
  
  
    const [filterDate, setFilterDate] = useState(null);
    const [filterDeliveryDate, setDeliveryFilterDate] = useState(null);
    const [filterStatus, setFilterStatus] = useState(null);
  
    const handleFilterDateChange = (event) => {
      const selectedDate = event.target.value;
      setFilterDate(selectedDate);
    };
  
    const handleFilterDeliveryDateChange = (event) => {
      const selectedDate = event.target.value;
      setDeliveryFilterDate(selectedDate);
    };
  
    const handleFilterStatusChange = (event) => {
      const selectedStatus = event.target.value;
      setFilterStatus(selectedStatus);
    };
  
    const resetFilters = () => {
      setFilterDate(null);
      setDeliveryFilterDate(null);
      setFilterStatus(null);
    };
  
    const filteredRequests = requests.filter((offer) => {
      return (
        (!filterDate || offer.pickupDate.split('T')[0] === filterDate ) &&
        (!filterDeliveryDate || offer.deliveryDate.split('T')[0] === filterDeliveryDate ) &&
        (!filterStatus || offer.status === filterStatus)
      );
    });





      if(isAuthenticated)
      {

        if(role === "Courier")
        {
            return(
                <div className="App-header-courier">

                </div>
            );
        }
        if(role === "OfficeWorker")
        {
            return(
                <div className="App-header-officeWorker">

                </div>
            );
        }
      }
      else
      {
          return(
              <div className="Profile-header">
                  <Box mt={4} ml={4} p={3} component={Paper} elevation={3} sx={{ backgroundColor: '#e3f2fd', marginBottom: '50px' }}>
                  {profile && (
                    <div>
                      <Typography variant="h4" gutterBottom>
                        Hello, {profile?.fullName || 'User'}
                      </Typography>
                      <Typography variant="body1" gutterBottom>
                        Email: {profile?.email}
                      </Typography>
                      <Typography variant="body1" gutterBottom>
                        Company name: {profile?.companyName}
                      </Typography> 

                      <Typography variant="body1" gutterBottom>
                        Address: {profile["address"]["street"]} {profile["address"]["streetNumber"]}, {profile["address"]["flatNumber"]}, {' '}
                        {profile["address"]["postalCode"]} {profile["address"]["city"]}
                      </Typography>
                      <Typography variant="body1" gutterBottom>
                        Default source address: {profile["defaultSourceAddress"]["street"]} {profile["defaultSourceAddress"]["streetNumber"]}, {profile["defaultSourceAddress"]["flatNumber"]}, {' '}
                        {profile["defaultSourceAddress"]["postalCode"]} {profile["defaultSourceAddress"]["city"]}
                      </Typography> 
                    </div>
                  )}
                  </Box>

                  {profile && (
                    <div className="profile-list">

                      <Typography variant="h3" gutterBottom className='gray-text'>
                        Your orders
                      </Typography>
                      <Typography variant="h5" gutterBottom className='gray-text'>
                          Sorting
                      </Typography>
                      <div className="sort-buttons">
                        <Button
                          variant="contained"
                          color="primary"
                          onClick={() => handleSortBy('pickupDate')}
                        >
                          Sort by Pickup Date
                        </Button>
                        <Button
                          variant="contained"
                          color="primary"
                          onClick={() => handleSortBy('deliveryDate')}
                        >
                          Sort by Delivery Date
                        </Button>
                        <Button
                          variant="contained"
                          color="primary"
                          onClick={() => handleSortBy('status')}
                        >
                          Sort by Status
                        </Button>
                    </div>
                    <Typography variant="h5" gutterBottom className='gray-text'>
                          Filtering
                        </Typography>
                    <div className="button-container">
                      <TextField
                        id="filter-date"
                        label="Filter by pickup date"
                        type="date"
                        variant="outlined"
                        InputLabelProps={{
                          shrink: true,
                        }}
                        value={filterDate || ''}
                        onChange={handleFilterDateChange}
                      />
                      <TextField
                        id="filter-date"
                        label="Filter by delivery date"
                        type="date"
                        variant="outlined"
                        InputLabelProps={{
                          shrink: true,
                        }}
                        value={filterDate || ''}
                        onChange={handleFilterDeliveryDateChange}
                      />
                      <TextField
                        id="filter-status"
                        label="Filter by Status"
                        select
                        variant="outlined"
                        value={filterStatus || ''}
                        onChange={handleFilterStatusChange}
                        style={{ minWidth: '150px' }} 
                      >
                        <MenuItem value="" disabled>
                          Select Status
                        </MenuItem>
                        <MenuItem value={0}>Pending</MenuItem>
                        <MenuItem value={1}>Accepted</MenuItem>
                        <MenuItem value={2}>Received</MenuItem>
                        <MenuItem value={3}>Delivered</MenuItem>
                        <MenuItem value={4}>Cannot Deliver</MenuItem>
                        <MenuItem value={5}>Cancelled</MenuItem>
                      </TextField>
                      <Button
                        variant="contained"
                        color="primary"
                        onClick={resetFilters}
                      >
                        Reset Filters
                      </Button>
                    </div>
                    <List>
                      {filteredRequests.map((offer) => (
                        <ListItem key={offer.id} className="list-item">
                          <ListItemText
                            primary={`ID: ${offer.id}`}
                            secondary={`Date: ${offer.pickupDate && new Date(offer.pickupDate).toLocaleDateString()} - 
                                ${offer.deliveryDate && new Date(offer.deliveryDate).toLocaleDateString()}`}
                            className="list-item-text"
                          />
                          <ListItemText
                            secondary={`Status: ${getStatusText(offer.status)}`}
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


                  )}

              </div>
          );
      }
      return (
        <div>
            <label>Hello {profile} </label>
        </div>);
}