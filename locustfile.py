from locust import HttpUser, task, between


class SmartHomeUser(HttpUser):
    wait_time = between(1, 2)

    @task
    def swagger_json(self):
        self.client.get("/swagger/v1/swagger.json", name="GET /swagger/v1/swagger.json")