import React from 'react';
import Navbar from 'react-bootstrap/Navbar'
import NavDropdown from 'react-bootstrap/NavDropdown';
import Nav from 'react-bootstrap/Nav';
import './NavMenu.css';
import { UserManager } from './UserManager';

export class NavMenu extends React.Component<{ User: any }> {
    static displayName = NavMenu.name;

    render() {
        return (
            <header>
                <Navbar collapseOnSelect className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" >
                    <Navbar.Brand href="/">u-ProMIS</Navbar.Brand>
                    <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                    <Navbar.Collapse id="responsive-navbar-nav">
                        <Nav className="mr-auto">
                            <Nav.Item>
                                <Nav.Link className="text-dark" href="/counter">Counter</Nav.Link>
                            </Nav.Item>
                            {
                                !!this.props.User &&
                                (this.props.User.profile.IsContractOwner
                                    || this.props.User.profile.IsContractParticipant
                                    || this.props.User.profile.IsContractReader) &&
                                <NavDropdown title="Contract" id="collapsable-contract-dropdown" disabled={!this.props.User} >
                                    <NavDropdown.Item className="text-dark" href='/contract' >
                                        Contracts
                                    </NavDropdown.Item>
                                    <NavDropdown.Divider />
                                    <NavDropdown.Item className="text-dark" href='/request' >
                                        Requests
                                    </NavDropdown.Item>
                                    <NavDropdown.Item className="text-dark" href='/proposal' >
                                        Proposals
                                    </NavDropdown.Item>
                                    <NavDropdown.Divider />
                                    <NavDropdown.Item className="text-dark" href='/client' >
                                        Clients
                                    </NavDropdown.Item>
                                </NavDropdown>
                            }
                            {
                                !!this.props.User &&
                                (this.props.User.profile.IsProjectOwner
                                    || this.props.User.profile.IsProjectParticipant
                                    || this.props.User.profile.IsProjectReader) &&
                                <NavDropdown title="Project" id="collapsable-contract-dropdown" disabled={!this.props.User}>
                                    <NavDropdown.Item className="text-dark" href='/project' >
                                        Projects
                                    </NavDropdown.Item>
                                    <NavDropdown.Item className="text-dark" href='/workpackage' >
                                        Workpackages
                                    </NavDropdown.Item>
                                    <NavDropdown.Item className="text-dark" href='/activity' >
                                        Activities
                                    </NavDropdown.Item>
                                    <NavDropdown.Item className="text-dark" href='/event' >
                                        Events
                                    </NavDropdown.Item>
                                    <NavDropdown.Item className="text-dark" href='/mission' >
                                        Missions
                                    </NavDropdown.Item>
                                    <NavDropdown.Divider />
                                </NavDropdown>
                            }
                            {!!this.props.User && this.props.User.profile.IsInAdminRole &&
                                <NavDropdown title="Config" id="collapsable-contract-dropdown" disabled={!this.props.User}>
                                    <NavDropdown.Item className="text-dark" href='/usermanager' >
                                        User manager
                                    </NavDropdown.Item>
                                    <NavDropdown.Divider />
                                </NavDropdown>}
                        </Nav>
                        <Nav className="mr-auto justify-content-center">
                            {
                                !!this.props.User ?
                                    <NavDropdown title={"Logged in as: " + this.props.User.profile.name} id="collapsable-contract-dropdown" >
                                        <NavDropdown.Item className="text-dark" title="Change your password" href='/changepassword' >Change Password</NavDropdown.Item>
                                        <NavDropdown.Item
                                            className="text-dark" onClick={() => {
                                                let mgr = new UserManager();
                                                mgr.Logout();
                                            }} title="Logout" >Logout
                                        </NavDropdown.Item>
                                    </NavDropdown>
                                    :
                                    <Nav.Item>
                                        <Nav.Link className="text-dark" onClick={() => {
                                            let mgr = new UserManager();
                                            mgr.Login();
                                        }} >Log In</Nav.Link>
                                    </Nav.Item>
                            }
                            <NavDropdown title="Help" id="collapsable-contract-dropdown">
                                <NavDropdown.Item className="text-dark" href='/help' >
                                    Help
                                </NavDropdown.Item>
                                <NavDropdown.Divider />
                                <NavDropdown.Item className="text-dark" href='/about' >
                                    About
                                </NavDropdown.Item>
                            </NavDropdown>
                        </Nav>
                    </Navbar.Collapse>
                </Navbar>
            </header >
        );
    }
}
