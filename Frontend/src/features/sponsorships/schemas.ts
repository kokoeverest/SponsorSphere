import * as yup from "yup";

const CreateSponsorshipSchema = yup.object().shape( {
    athleteId: yup.number().required( 'Athlete id is required' ),
    sponsorId: yup.number().required( 'Sponsor id is required' ),
    amount: yup.number().required().min(0),
    level: yup.string().required(),
} );

export default CreateSponsorshipSchema;