export async function getAllWorkoutsByProgramIdAsync(programId) {
  const response = await fetch(`/api/workout/program/${programId}`);
  return await response.json();
}

export async function createWorkoutAsync(workout) {
  const response = await fetch(`/api/workout`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(workout)
  })
  return await response.json();
}

export async function updateWorkoutAsync(workout) {
  const response = await fetch(`/api/workout/${workout.id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(workout)
  })
  return await response.json();
}

export async function deleteWorkoutAsync(workoutId) {
  await fetch(`/api/workout/${workoutId}`, {
    method: 'DELETE',
    headers: { 'Content-Type': 'application/json' }
  })
}
