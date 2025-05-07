type Args = {
    status: "success" | "error" | "pending" ;
}

const ApiStatus = ({status}: Args) => {
    switch(status) {
        case "error":
            return <div>Error communicating with the server</div>;
        case "pending":
            return (
                <div className="spinner-border text-primary" role="status">
                    <span className="visually-hidden">Loading...</span>
                </div>
            )
        default:
            throw Error("Unknown API state");
    }
};

export default ApiStatus;