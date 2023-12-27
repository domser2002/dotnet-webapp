import {NavLink, Outlet} from "react-router-dom";
import './App.css';
import {Button} from '@mui/material';
import { LoginButton } from "./LoginButton";
import { LogoutButton } from "./LogoutButton";
import IconButton from '@mui/material/IconButton';
import { RoleButton } from "./RoleButton";

export function Layout() {
    return(
        <div id="lay" >
            <div className='Navibar' style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <div>
                    {/* <NavLink to={"/"}>
                        <Button 
                        variant="contained" 
                        sx={{color: 'white', backgroundColor: 'rgb(45, 45, 45)',}}
                        
                        >Main page</Button>
                    </NavLink> */}
                    <NavLink to={"/"}>
                        <IconButton color="primary">
                            <img src='./wydraTransparent.png' alt='wydra' style={{ width: '60px', height: '60px' }}></img>
                        </IconButton>
                    </NavLink>

                    <span>{'     '}</span>
                    {/* <NavLink to={"/form"}>
                        <Button variant="contained" sx={{color: 'white', backgroundColor: 'rgb(45, 45, 45)',}}>Send Package</Button>
                    </NavLink> */}
                    <RoleButton></RoleButton>
                    <NavLink to={"/contactInformation"}>
                        <Button variant="contained" sx={{color: 'white', backgroundColor: 'rgb(45, 45, 45)',}}>Contact information</Button>
                    </NavLink>
                </div>
                <div>
                    {/* <NavLink to={"/login"}>
                        <Button variant="contained" sx={{color: 'white', backgroundColor: 'rgb(45, 45, 45)',}}>Login</Button>
                    </NavLink>
                    <NavLink to={"/register"}>
                        <Button variant="contained" sx={{color: 'white', backgroundColor: 'rgb(45, 45, 45)',}}>Register</Button>
                    </NavLink> */}
                    <LoginButton/>
                    <LogoutButton/>
                </div>

            </div>

            <Outlet/>
        </div>
    );
}
