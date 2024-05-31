import * as yup from 'yup';

export const CreatePastSportEventSchema = yup.object().shape( {
    name: yup.string().min( 2 ).max( 200 ).required( 'Name is required' ),
    country: yup.string().required( 'Country is required' ),
    eventDate: yup.date()
                .required( 'Event date is required' )
                .typeError( 'Event date must be a valid date' )
                .max( new Date(), 'Event date must be in the past' ),
    eventType: yup.string().required( 'Event type is required' ),
    sport: yup.string().required( 'Sport is required' ),
} );

export const CreateFutureSportEventSchema = yup.object().shape( {
    name: yup.string().min( 2 ).max( 200 ).required( 'Name is required' ),
    country: yup.string().required( 'Country is required' ),
    eventDate: yup.date()
        .required( 'Event date is required' )
        .typeError( 'Event date must be a valid date' )
        .min( new Date(), 'Event date must be in the future' ),
    eventType: yup.string().required( 'Event type is required' ),
    sport: yup.string().required( 'Sport is required' ),
} );