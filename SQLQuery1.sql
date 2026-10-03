SELECT * FROM USERS;
SELECT TOP 50 
    st.text AS [SQL Code Chay],
    qs.execution_count AS [So Lan Chay],
    qs.last_execution_time AS [Thoi Gian Chay Gan Nhat]
FROM 
    sys.dm_exec_query_stats qs
CROSS APPLY 
    sys.dm_exec_sql_text(qs.sql_handle) st
ORDER BY 
    qs.last_execution_time DESC;

