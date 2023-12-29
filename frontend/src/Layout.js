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
                    <NavLink to={"/"}>
                        <IconButton color="primary">
                            <img src='./wydraTransparent.png' alt='wydra' style={{ width: '60px', height: '60px' }}></img>
                        </IconButton>
                    </NavLink>
                    <span>{'     '}</span>
                    <RoleButton></RoleButton>
                </div>
                <div>
                    <LoginButton/>
                    <LogoutButton/>
                </div>

            </div>

            <Outlet/>
        </div>
    );
}
