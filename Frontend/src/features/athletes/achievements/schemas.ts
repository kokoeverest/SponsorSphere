import * as yup from "yup";

const CreateAchievementSchema = yup.object().shape( {
    sportEventId: yup.number().required( 'Sport event is required' ),
    placeFinished: yup.number().optional(),
    description: yup.string().optional(),
} );

export default CreateAchievementSchema;