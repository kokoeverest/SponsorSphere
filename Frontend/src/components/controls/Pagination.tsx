import React from 'react';
import { Pagination, PaginationItem } from '@mui/material';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import ArrowForwardIcon from '@mui/icons-material/ArrowForward';

interface StyledPaginationProps
{
    pageNumber: number;
    pageCount: number;
    onPageChange: ( event: React.ChangeEvent<unknown>, page: number ) => void;
}

const StyledPagination: React.FC<StyledPaginationProps> = ( { pageCount, pageNumber, onPageChange } ) => (
    <Pagination
        variant='text'
        shape='circular'
        size='small'
        showFirstButton
        showLastButton
        page={ pageNumber }
        count={ pageCount }
        onChange={ onPageChange }
        sx={ { display: 'flex', justifyContent: 'center', marginTop: 2 } }
        renderItem={ ( item ) => (
            <PaginationItem
                slots={ { previous: ArrowBackIcon, next: ArrowForwardIcon } }
                { ...item }
            />
        ) }
    />
);

export default StyledPagination;