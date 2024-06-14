import * as React from 'react';
import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';
import AthleteList from '@/features/athletes/AthleteList';
import CompaniesList from '@/features/sponsors/companies/CompaniesList';
import IndividualsList from '@/features/sponsors/individuals/IndividualsList';
import AuthContext from '@/context/AuthContext';
import PendingSportEventsList from '@/features/admins/PendingSportEventsList';

interface TabPanelProps
{
    children?: React.ReactNode;
    index: number;
    value: number;
}

function CustomTabPanel ( props: TabPanelProps )
{
    const { children, value, index, ...other } = props;

    return (
        <div
            role="tabpanel"
            hidden={ value !== index }
            id={ `simple-tabpanel-${ index }` }
            aria-labelledby={ `simple-tab-${ index }` }
            { ...other }
            style={ { width: '100%', height: '500px' } }
        >
            { value === index && (
                <Box sx={ { p: 3, height: '100%' } }>
                    <Typography component={ 'div' } sx={ { height: '100%', width: '600px' } }>{ children }</Typography>
                </Box>
            ) }
        </div>
    );
}

function a11yProps ( index: number )
{
    return {
        id: `simple-tab-${ index }`,
        'aria-controls': `simple-tabpanel-${ index }`,
    };
}


export default function Feed ()
{
    const [ value, setValue ] = React.useState( 0 );
    const { role } = React.useContext( AuthContext );

    const handleChange = ( _: React.SyntheticEvent, newValue: number ) =>
    {
        setValue( newValue );
    };

    return (
        <Box sx={ {
            marginLeft: '240px',
            m: 'auto'
        } }>
            <Box sx={ {
                borderBottom: 1,
                borderLeft: 0,
                borderRight: 0,
                borderColor: 'divider'
            } }>
                <Tabs value={ value } onChange={ handleChange } aria-label="basic tabs example" centered>
                    <Tab label="Athletes" { ...a11yProps( 0 ) } />
                    <Tab label="Sponsors: Companies" { ...a11yProps( 1 ) } />
                    <Tab label="Sponsors: Individuals" { ...a11yProps( 2 ) } />
                    { role === 'Admin' && <Tab label='Pending Sport Events' { ...a11yProps( 3 ) } /> }
                </Tabs>
            </Box>
            <CustomTabPanel value={ value } index={ 0 }>
                <AthleteList />
            </CustomTabPanel>
            <CustomTabPanel value={ value } index={ 1 }>
                <CompaniesList />
            </CustomTabPanel>
            <CustomTabPanel value={ value } index={ 2 }>
                <IndividualsList />
            </CustomTabPanel>
            <CustomTabPanel value={ value } index={ 3 }>
                <PendingSportEventsList />
            </CustomTabPanel>
        </Box>
    );
}