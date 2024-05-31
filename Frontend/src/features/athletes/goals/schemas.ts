import * as yup from "yup";

const CreateGoalSchema = yup.object().shape( {
    sportEventId: yup.number().required( 'Sport event is required' ),
    amountNeeded: yup.number().min(1).required('Amount is required'),
} );

export default CreateGoalSchema;