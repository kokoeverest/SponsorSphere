import * as yup from 'yup';

const RegisterIndividualSchema = yup.object().shape( {
    name: yup.string().min( 2 ).max( 200 ).required( 'First name is required' ),
    lastName: yup.string().min( 2 ).max( 200 ).required( 'Last name is required' ),
    email: yup.string().email( 'Must be a valid email' ).required( 'Email is required' ),
    password: yup.string().min( 8 ).max( 32 ).required( 'Password is required' ),
    birthDate: yup.string().required( 'Birthdate is required' ).typeError( 'Invalid date format' ),
    phoneNumber: yup.string().required( 'Phone number is required' ),
    country: yup.string().required( 'Country is required' ),
} );

const UpdateSponsorIndividualProfileSchema = yup.object().shape( {
    id: yup.number().required( 'Id is required' ),
    name: yup.string().min( 2 ).max( 200 ).required( "First name is required" ),
    lastName: yup.string().min( 2 ).max( 200 ).required( "Last name is required" ),
    email: yup
        .string()
        .email( "Must be a valid email" )
        .required( "Email is required" ),
    birthDate: yup
        .date()
        .required( "Birthdate is required" )
        .typeError( "Invalid date format" ),
    phoneNumber: yup.string().required( "Phone number is required" ),
    country: yup.string().required( "Country is required" ),
    pictureId: yup.mixed().nullable().optional(),
    website: yup.string().nullable().optional(),
    faceBookLink: yup.string().nullable().optional(),
    instagramLink: yup.string().nullable().optional(),
    twitterLink: yup.string().nullable().optional(),
    stravaLink: yup.string().nullable().optional(),
} );

export { RegisterIndividualSchema, UpdateSponsorIndividualProfileSchema };