import * as yup from 'yup';

const RegisterCompanySchema = yup.object().shape( {
    name: yup.string().min( 2 ).max( 200 ).required( 'First name is required' ),
    iban: yup.string().min( 15 ).max( 34 ).required( 'Valid IBAN is required' ),
    email: yup.string().email( 'Must be a valid email' ).required( 'Email is required' ),
    password: yup.string().min( 8 ).max( 32 ).required( 'Password is required' ),
    phoneNumber: yup.string().required( 'Phone number is required' ),
    country: yup.string().required( 'Country is required' ),
} );

const UpdateSponsorCompanyProfileSchema = yup.object().shape( {
    id: yup.number().required( 'Id is required' ),
    name: yup.string().min( 2 ).max( 200 ).required( "First name is required" ),
    email: yup
        .string()
        .email( "Must be a valid email" )
        .required( "Email is required" ),
    phoneNumber: yup.string().required( "Phone number is required" ),
    country: yup.string().required( "Country is required" ),
    iban: yup.string().required( "Enter a valid IBAN" ),
    pictureId: yup.mixed().nullable().optional(),
    website: yup.string().nullable().optional(),
    faceBookLink: yup.string().nullable().optional(),
    instagramLink: yup.string().nullable().optional(),
    twitterLink: yup.string().nullable().optional(),
    stravaLink: yup.string().nullable().optional(),
} );

export { RegisterCompanySchema, UpdateSponsorCompanyProfileSchema };