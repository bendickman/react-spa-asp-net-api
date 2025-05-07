import { useParams } from "react-router-dom";
import { useFetchHouse, useUpdateHouse } from "../hooks/HouseHooks";
import HouseForm from "./HouseForm";
import ApiStatus from "../apiStatus";
import ValidationSummary from "../ValidationSummary";

const HouseUpdate = () => {
    const { id } = useParams();
    if (!id) {
        throw Error("No house id supplied");
    }

    const houseId = parseInt(id);

    const { data: house, status, isSuccess } = useFetchHouse(houseId);
    const updateHouseMutation = useUpdateHouse();

    if (!isSuccess) {
        return (
            <ApiStatus status={status} />
        )
    }

    return (
        <>
            {updateHouseMutation.error && (
                <ValidationSummary error={updateHouseMutation.error} />
            )}
            <HouseForm 
                house={house}
                submitted={(h) => updateHouseMutation.mutate(h)} 
            />
    </>
    )
}

export default HouseUpdate;