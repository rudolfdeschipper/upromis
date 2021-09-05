import React, { Suspense } from 'react';
import { Route, Switch } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Counter } from './components/Counter';
import nfp from './components/NotFoundPage';
import { UserManager } from './components/UserManager';
import { UserContext } from './context/UserContext';


interface IProps {
}


interface IState {
    _user: Oidc.User | null;
}

class App extends React.Component<IProps, IState> {
    static displayName = App.name;

    constructor(props: IProps, state: IState) {
        super(props);
        this.state = {
            _user: null
        };
    }

    componentDidMount() {
        let mgr = new UserManager();
        mgr.GetUser().then((user) => {
            this.setState({
                _user: user
            });
            console.log(user);
        })

    }

    canAccessContract() : boolean
    {
        return !!this.state._user
        && (this.state._user.profile.IsContractOwner
        || this.state._user.profile.IsContractParticipant
        || this.state._user.profile.IsContractReader);
    }

    canManageContract() : boolean
    {
        return !!this.state._user
        && (this.state._user.profile.IsContractOwner
        );
    }

    canAccessProject() : boolean
    {
        return !!this.state._user
        && (this.state._user.profile.IsProjectOwner
        || this.state._user.profile.IsProjectParticipant
        || this.state._user.profile.IsProjectReader);
    }

    render() {
        const contract = React.lazy(() => import("./components/Contract/generated/Contract"));
        const contractdetails = React.lazy(() => import("./components/Contract/generated/ContractDetails"));
        const request = React.lazy(() => import("./components/Request/generated/Request"));
        const requestdetails = React.lazy(() => import("./components/Request/generated/RequestDetails"));
        const proposal = React.lazy(() => import("./components/Proposal/generated/Proposal"));
        const proposaldetails = React.lazy(() => import("./components/Proposal/generated/ProposalDetails"));
        const client = React.lazy(() => import("./components/Client/generated/Client"));
        const clientdetails = React.lazy(() => import("./components/Client/generated/ClientDetails"));
        const userManager = React.lazy(() => import("./components/UserManager/generated/UserManager"));
        const userManagerdetails = React.lazy(() => import("./components/UserManager/generated/UserManagerDetails"));

        return (
            <Layout User={this.state._user}>
                <UserContext.Provider value={this.state._user}>
                    <Suspense fallback={<div className="page-container">Loading...</div>}>
                        <Switch>
                            <Route exact path='/' component={Home} />
                            <Route path='/home' component={Home} />
                            <Route path='/counter' component={Counter} />
                            {this.canAccessContract() && <Route path='/contract' component={contract} /> }
                            {this.canAccessContract() && <Route path='/contractdetails/:id' component={contractdetails} /> }
                            {this.canAccessContract() && <Route path='/contractdetails/add' component={contractdetails} /> }
                            {this.canAccessContract() && <Route path='/proposal' component={proposal} /> }
                            {this.canAccessContract() && <Route path='/proposaldetails/:id' component={proposaldetails} /> }
                            {this.canAccessContract() && <Route path='/proposaldetails/add' component={proposaldetails} /> }
                            {this.canAccessContract() && <Route path='/request' component={request} /> }
                            {this.canAccessContract() && <Route path='/requestdetails/:id' component={requestdetails} /> }
                            {this.canAccessContract() && <Route path='/requeqstdetails/add' component={requestdetails} /> }
                            {this.canManageContract() && <Route path='/client' component={client} /> }
                            {this.canManageContract() && <Route path='/clientdetails/:id' component={clientdetails} /> }
                            {this.canManageContract() && <Route path='/clientdetails/add' component={clientdetails} /> }
                            {!!this.state._user && this.state._user.profile.IsInAdminRole && <Route path='/usermanager' component={!!this.state._user ? userManager : nfp} /> }
                            {!!this.state._user && this.state._user.profile.IsInAdminRole && <Route path='/usermanagerdetails/:id' component={!!this.state._user ? userManagerdetails : nfp} /> }
                            {!!this.state._user && this.state._user.profile.IsInAdminRole && <Route path='/usermanagerdetails/add' component={!!this.state._user ? userManagerdetails : nfp} /> }
                            <Route path="*" component={nfp} />
                        </Switch>
                    </Suspense>
                </UserContext.Provider>
            </Layout>
        );
    }
}


export default App;
