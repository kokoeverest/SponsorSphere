import * as yup from "yup";

const CreateAchievementSchema = yup.object().shape( {
    sportEventId: yup.number().required( 'Sport event is required' ),
    placeFinished: yup.number(),
    description: yup.string(),
} );

export default CreateAchievementSchema;