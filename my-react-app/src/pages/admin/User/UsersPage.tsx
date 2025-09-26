import axios from 'axios';
import * as React from 'react';
import { useEffect, useState } from 'react';
import type { IUserItem } from './UserInterface';
import UserRow from './UserRow';
import EnvConfig from '../../../config/env';

const UsersList : React.FC = () => {
    const urlGet = `${EnvConfig.API_URL}/api/User/list`;

    const [users, setUsers] = useState<IUserItem[]>([]);

    useEffect(() => {
        axios.get(urlGet)
            .then(response => {
                console.log("Users:", response.data);
                setUsers(response.data.payload);
            })
            .catch(error => {
                console.error("Error fetching users:", error);
            });
        console.log("Working useEffect", urlGet);
    }, []);

    const initials = (name: string) =>
        name
            .split(" ")
            .filter(Boolean)
            .map((n) => n[0]?.toUpperCase())
            .slice(0, 2)
            .join("");

    return (
        <>
            <div className="w-full">
                <div className="hidden md:block overflow-x-auto">
                    <div
                        className="bg-white dark:bg-neutral-900 rounded-2xl shadow-sm ring-1 ring-black/5 overflow-hidden">
                        <div
                            className="flex items-center justify-between gap-3 px-5 py-4 border-b border-black/10 dark:border-white/10">
                            <h2 className="text-lg font-semibold">Користувачі</h2>
                            <div className="text-sm text-neutral-500">{users.length} запис(ів)</div>
                        </div>

                        <div className="overflow-x-auto">
                            <table className="min-w-full text-left align-middle">
                                <thead
                                    className="bg-neutral-50 dark:bg-neutral-800 text-neutral-600 dark:text-neutral-300 text-xs uppercase tracking-wider">
                                <tr>
                                    <th className="px-5 py-3 font-semibold">ID</th>
                                    <th className="px-5 py-3 font-semibold">Користувач</th>
                                    <th className="px-5 py-3 font-semibold">Email</th>
                                    <th className="px-5 py-3 font-semibold">Ролі</th>
                                    <th className="px-5 py-3 font-semibold text-right">Дії</th>
                                </tr>
                                </thead>
                                <tbody className="divide-y divide-black/5 dark:divide-white/10">
                                {users.map((u) => (
                                    <UserRow key={u.id} user={u} initials={initials} />
                                ))}
                                </tbody>

                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};

export default UsersList;
