import { useParams } from "react-router-dom";

const UserView: React.FC = () => {
    const { id } = useParams<{ id: string }>();

    return (
        <div>
            <h1>User {id}</h1>
        </div>
    );
};

export default UserView;
